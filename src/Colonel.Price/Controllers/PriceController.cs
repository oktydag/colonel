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
        private readonly IPriceRepository _priceRepository;

        public PriceController(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        [HttpGet]
        [Route("")]
        [Produces("application/json")]
        public async iTask<IActionResult> GetProductPrice([FromQuery] PriceRequestModel priceRequestModel)
        {
            var price = await _priceRepository.GetPriceByProductId(priceRequestModel);
            
            if(price == null)
                return NotFound($"The Price whose Product ID is equal to {priceRequestModel.ProductId} cannot be found.");

            return Ok(price);
        }
           

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<List<Price>>> Get() =>
         await _priceRepository.GetAllPrices();


    }
}