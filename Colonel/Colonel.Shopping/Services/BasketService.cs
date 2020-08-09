using Colonel.Shopping.Entities;
using Colonel.Shopping.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Colonel.Shopping.Models;
using Colonel.Shopping.Models.Price;
using Colonel.Shopping.Models.Product;
using Colonel.Shopping.Models.Stock;
using Colonel.Shopping.Models.User;
using Colonel.Shopping.Providers;
using Colonel.Shopping.Core.Exceptions;

namespace Colonel.Shopping.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductService _productService;
        private readonly IPriceService _priceService;
        private readonly IStockService _stockService;
        private readonly IUserService _userService;

        public BasketService(IBasketRepository basketRepository, IProductService productService, IPriceService priceService, IStockService stockService, IUserService userService)
        {
            _basketRepository = basketRepository;
            _productService = productService;
            _priceService = priceService;
            _stockService = stockService;
            _userService = userService;
        }

        public bool AddToBasket(AddProductToBasketRequestModel basketItems)
        {
            var productAvailability = CheckProductIsAvailable(basketItems);
            if (!productAvailability.IsAvailable)
                throw new ProductIsNotAvailableException("Product is not available ! Check Logs. ");

            var userBasket = _basketRepository.GetUserBasket(basketItems.UserId);
            if (userBasket == null)
            {
                userBasket = CreateNewBasket(basketItems, productAvailability);
            }
            else
            {
                UpdateBasket(basketItems, productAvailability, userBasket);
            }

            _basketRepository.SaveBasket(userBasket);


            // TODO: Send event as  NewAddToCartServiceWork.

            return true;

        }

        public Basket SaveBasket(Basket basket)
        {
            return _basketRepository.SaveBasket(basket);
        }

        public Basket GetUserBasket(int userId)
        {
            return _basketRepository.GetUserBasket(userId);
        }

        public ProductAvailability CheckProductIsAvailable(AddProductToBasketRequestModel basketItems)
        {
            var product = _productService.GetProduct(new ProductRequestModel() { ProductId = basketItems.ProductId });
            if (product == null)
                return ProductAvailability.NotAvailable;

            var user = _userService.GetUser(new UserRequestModel() { UserId = basketItems.UserId });
            if (user == null)
                return ProductAvailability.NotAvailable;

            var stockModelForRequestQuantity = _stockService.HasAvailableStock(new StockRequestModel() { ProductId = basketItems.ProductId, Quantity = basketItems.Quantity });
            if (stockModelForRequestQuantity == null)
                return ProductAvailability.NotAvailable;

            var priceOfProduct = _priceService.GetProductPrice(new PriceRequestModel()
            { ProductId = basketItems.ProductId, RequestDate = DateTimeProvider.Instance.GetUtcNow() });
            if (priceOfProduct == null)
                return ProductAvailability.NotAvailable;

            return new ProductAvailability(stockModelForRequestQuantity.Id, priceOfProduct.Value, basketItems.ProductId, true);
        }

        public BasketLine CreateNewBasketLine(AddProductToBasketRequestModel basketItems, ProductAvailability productAvailability)
        {
            var basketLine = new BasketLine()
            {
                ProductId = basketItems.ProductId,
                Quantity = basketItems.Quantity,
                StockId = productAvailability.StockId,
                GiftNote = basketItems.GiftNote
            };

            return basketLine;
        }

        public Basket CreateNewBasket(AddProductToBasketRequestModel basketItems, ProductAvailability productAvailability)
        {
            
            List<BasketLine> basketLines = new List<BasketLine>();
            basketLines.Add(CreateNewBasketLine(basketItems,productAvailability));

            var basket = new Basket()
            {
                UserId = basketItems.UserId,
                CreatedDate = DateTime.UtcNow,
                BasketLines = basketLines,
                IsActive = true,
                IsOrdered = false,
                UpdateDate = DateTimeProvider.Instance.GetUtcNow()

            };

            return basket;
        }


        public Basket UpdateBasket(AddProductToBasketRequestModel basketItems, ProductAvailability productAvailability, Basket userBasket)
        {
            var addedBasketLine = userBasket.BasketLines.FirstOrDefault(x => x.ProductId == basketItems.ProductId);
            if (addedBasketLine != null)
            {
                var newQuantity = addedBasketLine.Quantity + basketItems.Quantity;

                var availableStockModelForNewQuantity = _stockService.HasAvailableStock(new StockRequestModel() { ProductId = basketItems.ProductId, Quantity = newQuantity });

                if (availableStockModelForNewQuantity == null)
                    throw new ProductIsNotAvailableException("Stock is not sufficient");

                addedBasketLine.Quantity = newQuantity;
                addedBasketLine.GiftNote = basketItems.GiftNote;

            }
            else
            {
                userBasket.BasketLines.Add(CreateNewBasketLine(basketItems, productAvailability));
            }

            return userBasket;
        }

    }
}
