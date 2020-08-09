using System;
using Colonel.Shopping.Core.Exceptions;
using Colonel.Shopping.Models;
using Colonel.Shopping.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Colonel.Shopping.Controllers
{
    [Route("api/v1/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<BasketController> _logger;


        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        public ActionResult AddToBasket([FromBody] AddProductToBasketRequestModel basketItems)
        {
            _logger.LogInformation("AddToBasket service initializer");

            try
            {
                var addToBasketResult = _basketService.AddToBasket(basketItems);

                _logger.LogInformation("AddToBasket finish own operation");

                return Ok();
            }
            catch (ProductIsNotAvailableException productIsNotAvailableException)
            {
                _logger.LogError(productIsNotAvailableException.Message);
                return StatusCode(406, productIsNotAvailableException.Message);
            }
            catch (Exception unExpectedException)
            {
                _logger.LogError(unExpectedException.Message);

                return StatusCode(500, unExpectedException.Message);
            }


        }
    }
}