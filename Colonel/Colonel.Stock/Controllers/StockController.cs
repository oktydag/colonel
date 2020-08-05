using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colonel.Stock.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Stock.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public ActionResult<List<Stock>> Get() =>
         _stockService.GetAllStock();

    }
}