using System;
using Colonel.Shopping.Core.Exceptions;
using Colonel.Shopping.Models;
using Colonel.Shopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Shopping.Controllers
{
    [Route("api/v1/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
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
            catch (ProductIsNotAvailableException productIsNotAvailableException)
            {
                return StatusCode(406, productIsNotAvailableException.Message);
            }
            catch (Exception unExpectedException)
            {
                return StatusCode(500, unExpectedException.Message);
            }
        }
    }
}