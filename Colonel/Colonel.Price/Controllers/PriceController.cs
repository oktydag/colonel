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
        public ActionResult<Price> GetProductPrice(int productId)
        {
            var price = _priceServices.GetPriceByProductId(productId);
            
            if(price == null) return NotFound($"The Price whose Product ID is equal to {productId} cannot be found.");
            return price;
        }
           

        [HttpGet]
        [Route("allprices")]
        public ActionResult<List<Price>> Get() =>
         _priceServices.GetAllPrices();


    }
}