using System.Collections.Generic;

namespace Colonel.Stock.Services
{
    public interface IStockService
    {
        Stock GetStockByProductId(string productId);

        List<Stock> GetAllStock();

    }
}
