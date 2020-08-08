using Colonel.Shopping.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Services
{
    public interface IStockService
    {
        StockResponseModel HasAvailableStock(StockRequestModel stockRequestModel);
    }
}
