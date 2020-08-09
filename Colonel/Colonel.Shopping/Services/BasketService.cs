using Colonel.Shopping.Entities;
using Colonel.Shopping.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colonel.Shopping.Entities;
using Colonel.Shopping.Models;
using Colonel.Shopping.Models.Price;
using Colonel.Shopping.Models.Product;
using Colonel.Shopping.Models.Stock;
using Colonel.Shopping.Models.User;
using Colonel.Shopping.Providers;
using Colonel.Shopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Shopping.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductService _productService;
        private readonly IPriceService _priceService;
        private readonly IStockService _stockService;
        private readonly IUserService _userService;
        private readonly IBasketService _basketService;

        public BasketService(IBasketRepository basketRepository, IProductService productService, IPriceService priceService, IStockService stockService, IUserService userService, IBasketService basketService)
        {
            _basketRepository = basketRepository;
            _productService = productService;
            _priceService = priceService;
            _stockService = stockService;
            _userService = userService;
            _basketService = basketService;
        }

        public Basket SaveBasket(Basket basket)
        {
            return _basketRepository.SaveBasket(basket);
        }

        public Basket GetUserBasket(int userId)
        {
            return _basketRepository.GetUserBasket(userId);
        }

        public bool AddToBasket(AddProductToBasketRequestModel basketItems)
        {
            var product = _productService.GetProduct(new ProductRequestModel() { ProductId = basketItems.ProductId });
            if (product == null) return false; //Custom Exception Onsale mi ?

            var user = _userService.GetUser(new UserRequestModel() { UserId = basketItems.UserId });
            if (user == null) return false; // Custom Exception isactive exception

            var stockModelForRequestQuantity = _stockService.HasAvailableStock(new StockRequestModel() { ProductId = basketItems.ProductId, Quantity = basketItems.Quantity });
            if (stockModelForRequestQuantity == null) return false;

            var priceOfProduct = _priceService.GetProductPrice(new PriceRequestModel()
            { ProductId = basketItems.ProductId, RequestDate = DateTimeProvider.Instance.GetUtcNow() });

            var basketLine = new BasketLine()
            {
                ProductId = basketItems.ProductId,
                Quantity = basketItems.Quantity,
                StockId = stockModelForRequestQuantity.Id,
                GiftNote = basketItems.GiftNote
            };

            var userBasket = _basketService.GetUserBasket(basketItems.UserId);
            if (userBasket == null)
            {
                List<BasketLine> basketLines = new List<BasketLine>();
                basketLines.Add(basketLine);

                var basket = new Basket()
                {
                    UserId = basketItems.UserId,
                    CreatedDate = DateTime.UtcNow,
                    BasketLines = basketLines,
                    IsActive = true,
                    IsOrdered = false,
                    UpdateDate = DateTimeProvider.Instance.GetUtcNow()

                };
                //add basket
                _basketService.SaveBasket(basket);
            }
            else
            {
                // If basket exist, check lines due to basketline is exist.
                var addedBasketLine = userBasket.BasketLines.FirstOrDefault(x => x.ProductId == basketItems.ProductId);
                if (addedBasketLine != null)
                {
                    var newQuantity = addedBasketLine.Quantity + basketItems.Quantity;

                    var availableStockModelForNewQuantity = _stockService.HasAvailableStock(new StockRequestModel() { ProductId = basketItems.ProductId, Quantity = newQuantity });

                    if (availableStockModelForNewQuantity == null)
                        return false; //Custom Exception

                    addedBasketLine.Quantity = newQuantity;
                    addedBasketLine.GiftNote = basketItems.GiftNote;

                }
                else
                {
                    userBasket.BasketLines.Add(basketLine);

                }
                _basketService.SaveBasket(userBasket);

            }


            // TODO: Send event as  NewAddToCartServiceWork.

            return true;


        }

    }
}
