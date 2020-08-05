using System.Collections.Generic;

namespace Colonel.Stock.Services
{
    public interface IStockService
    {
        List<Stock> GetAllStock();
        Stock GetStockByProductId(string productId);

    }
}
