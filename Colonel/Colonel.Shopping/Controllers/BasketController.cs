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
using Colonel.Shopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Shopping.Controllers
{
    [Route("api/v1/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IPriceService _priceService;
        private readonly IStockService _stockService;
        private readonly IUserService _userService;
        private readonly IBasketService _basketService;

        public BasketController(IProductService productService, IPriceService priceService, 
            IStockService stockService, IUserService userService, IBasketService basketService)
        {
            _productService = productService;
            _priceService = priceService;
            _stockService = stockService;
            _userService = userService;
            _basketService = basketService;
        }

        [HttpPost]
        [Route("addproducttobasket")]
        public ActionResult<bool> AddProductToBasket([FromBody] AddProductToBasketRequestModel basketItems)
        {
            var product = _productService.GetProduct(new ProductRequestModel() { ProductId = basketItems.ProductId });
            if(product == null) return false; //Custom Exception
            
            var stock = _stockService.HasAvailableStock(new StockRequestModel() { ProductId = basketItems.ProductId, Quantity = basketItems.Quantity });
            if (stock == null) return false; //Custom Exception

            var user = _userService.GetUser(new UserRequestModel() { UserId = basketItems.UserId });
            if (user == null) return false; // Custom Exception

            //TODO: datetime provider should implemented. 
            var priceOfProduct = _priceService.GetProductPrice(new PriceRequestModel()
            { ProductId = basketItems.ProductId, RequestDate = DateTime.UtcNow });

            var basketLine = new BasketLine()
            {
                ProductId = basketItems.ProductId,
                Quantity = basketItems.Quantity,
                StockId = stock.Id,
                GiftNote = basketItems.GiftNote
            };

            var userBasket = _basketService.GetUserBasket(basketItems.UserId);
            if(userBasket == null)
            {
                
                List<BasketLine> basketLines = new List<BasketLine>();
                basketLines.Add(basketLine);

                var basket = new Basket()
                {
                    UserId = basketItems.UserId,
                    CreatedDate = DateTime.UtcNow,
                    BasketLines = basketLines
                };

                //add basketLine
                _basketService.AddBasketLine(basketLine);
                //add basket
                _basketService.AddBasket(basket);
            }
            else
            {
                // If basket exist, check lines due to basketline is exist.
                var userBasketLines = userBasket.BasketLines.Where(x => x.ProductId == basketItems.ProductId).ToList();

                if(userBasketLines.Count == 0)
                    _basketService.AddBasketLine(basketLine);
                else
                {
                    var newQuantity = userBasketLines.FirstOrDefault().Quantity + basketItems.Quantity;
                    //_basketService.IncreaseQuantityOfProductInBasket(basketItems.UserId, newQuantity);

                    _basketService.AddBasketLine(basketLine);
                }
            }


            // TODO: Send event as  NewAddToCartServiceWork.


            return true;

        }
    }
}