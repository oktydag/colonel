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
        [Route("")]
        public ActionResult AddToBasket([FromBody] AddProductToBasketRequestModel basketItems)
        {
            try
            {
                var addToBasketResult = _basketService.AddToBasket(basketItems);

                return Ok();

            }
            catch (Exception)
            {
                return StatusCode(406, "error");
            }
        }
    }
}