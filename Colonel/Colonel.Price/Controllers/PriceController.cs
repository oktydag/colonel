using System.Collections.Generic;
using System.Threading.Tasks;
using Colonel.Price.Models;
using Colonel.Price.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Price.Controllers
{
    [Route("api/v1/prices")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceServices _priceServices;

        public PriceController(IPriceServices priceServices)
        {
            _priceServices = priceServices;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public async Task<ActionResult<Price>> GetProductPrice([FromQuery] PriceRequestModel priceRequestModel)
        {
            //TODO : model is valid kontrolü

            var price = await _priceServices.GetPriceByProductId(priceRequestModel);
            
            if(price == null)
                return NotFound($"The Price whose Product ID is equal to {priceRequestModel.ProductId} cannot be found.");

            return price;
        }
           

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<List<Price>>> Get() =>
         await _priceServices.GetAllPrices();


    }
}