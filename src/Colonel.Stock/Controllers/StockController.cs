﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Colonel.Stock.Models;
using Colonel.Stock.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Stock.Controllers
{
    [Route("api/v1/stocks")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet("")]
        [Produces("application/json")]
        public async Task<IActionResult> GetProductStockCount([FromQuery] StockRequestModel stockRequestModel) {
            var stock = await _stockRepository.GetStockByProductId(stockRequestModel.ProductId);

            if(stock == null)
                return NotFound($"The Stock whose Product ID is equal to {stockRequestModel.ProductId} cannot be found.");

            const int MIN_STOCK_VALUE = 1;
            if (stock.Value < MIN_STOCK_VALUE || stock.Value < stockRequestModel.Quantity)
                return NotFound($"The Stock whose Product ID is equal to {stockRequestModel.ProductId} has no available stock !");

            return Ok(stock);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Stock>>> Get() =>
         await _stockRepository.GetAllStock();



    }
}