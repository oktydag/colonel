using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colonel.Shopping.Entities;
using Colonel.Shopping.Models;
using Colonel.Shopping.Models.Price;
using Colonel.Shopping.Models.Product;
using Colonel.Shopping.Models.Stock;
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

        public BasketController(IProductService productService, IPriceService priceService, IStockService stockService)
        {
            _productService = productService;
            _priceService = priceService;
            _stockService = stockService;
        }

        [HttpPost]
        [Route("addproducttobasket")]
        public ActionResult<bool> AddProductToBasket([FromBody] AddProductToBasketRequestModel basketItems)
        {
            var product = _productService.GetProduct(new ProductRequestModel() { ProductId = basketItems.ProductId });
            if(product == null) return false; //Custom Exception
            
            var stock = _stockService.HasAvailableStock(new StockRequestModel() { ProductId = basketItems.ProductId, Quantity = basketItems.Quantity });
            if (stock == null) return false; //Custom Exception


            //TODO: datetime provider should implemented. 
            var priceOfProduct = _priceService.GetProductPrice(new PriceRequestModel()
            { ProductId = basketItems.ProductId, RequestDate = DateTime.UtcNow });



            return true;

        }
    }
}