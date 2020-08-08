using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colonel.Shopping.Entities;
using Colonel.Shopping.Models.Price;
using Colonel.Shopping.Models.Product;
using Colonel.Shopping.Models.Stock;
using Colonel.Shopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Shopping.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _addProductToBasketService;
        public BasketController(IBasketService addProductToBasketService)
        {
            _addProductToBasketService = addProductToBasketService;
        }

        [HttpGet]
        [Route("addproducttobasket")]
        public ActionResult<bool> AddProductToBasket(BasketItems basketItems)
        {
            //TODO : Prod uct Service OnSale Control
            var product = _addProductToBasketService.CheckProductOnSale(new ProductRequestModel() { ProductId = basketItems.ProductId });

            if(product == null) return false;
            if (!product.OnSale) return false; //exception

            // TODO: Stock Service Quantity
            var stock = _addProductToBasketService.CheckProductHasStock(new StockRequestModel() { ProductId = basketItems.ProductId });

            if (stock == null) return false;
            if (stock.Value < 1 && stock.Value < basketItems.Quantity) return false; //exception


            // TODO: Price Service - Price as Date

            var priceOfProduct = _addProductToBasketService.GetProductPriceByDate(new PriceRequestModel()
            { ProductId = basketItems.ProductId});

            if (priceOfProduct == null) return false;
            // tarih aralığı kontrolü


            return true;

        }
    }
}