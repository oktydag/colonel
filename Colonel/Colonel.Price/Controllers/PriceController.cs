using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colonel.Price.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Price.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceServices _priceServices;

        public PriceController(IPriceServices priceServices)
        {
            _priceServices = priceServices;
        }

        [HttpGet]
        [Route("pricebyproductid")]
        public ActionResult<Price> GetProductPrice(string productId) =>
            _priceServices.GetPriceByProductId(productId);

        [HttpGet]
        [Route("allprices")]
        public ActionResult<List<Price>> Get() =>
         _priceServices.GetAllPrices();


    }
}