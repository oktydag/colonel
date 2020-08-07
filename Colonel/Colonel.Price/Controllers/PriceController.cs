using System.Collections.Generic;
using Colonel.Price.Models;
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
        public ActionResult<Price> GetProductPrice(PriceRequestModel priceRequestModel)
        {
            //TODO : model is valid kontrolü

            var price = _priceServices.GetPriceByProductId(priceRequestModel.ProductId);
            
            if(price == null) return NotFound($"The Price whose Product ID is equal to {priceRequestModel.ProductId} cannot be found.");
            return price;
        }
           

        [HttpGet]
        [Route("allprices")]
        public ActionResult<List<Price>> Get() =>
         _priceServices.GetAllPrices();


    }
}