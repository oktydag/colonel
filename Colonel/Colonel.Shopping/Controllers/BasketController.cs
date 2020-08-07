using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colonel.Shopping.Entities;
using Colonel.Shopping.Models.Price;
using Colonel.Shopping.Models.Product;
using Colonel.Shopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Shopping.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IAddProductToBasketService _addProductToBasketService;
        public BasketController(IAddProductToBasketService addProductToBasketService)
        {
            _addProductToBasketService = addProductToBasketService;
        }

        [HttpGet]
        [Route("addproducttobasket")]
        public ActionResult<bool> AddProductToBasket(BasketItems basketItems)
        {
            //TODO : Prod uct Service OnSale Control
            var product = _addProductToBasketService.CheckProductOnSale(new ProductRequestModel() { ProductId = basketItems.ProductId });

            if (product == null) return false;
            // TODO: Stock Service Quantity

            // TODO: Price Service - Price as Date



            return true;

        }
    }
}