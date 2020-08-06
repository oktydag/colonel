using System.Collections.Generic;

namespace Colonel.Stock.Services
{
    public interface IStockService
    {
        Stock GetStockByProductId(int productId);

        List<Stock> GetAllStock();

    }
}
