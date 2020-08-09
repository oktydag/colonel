using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colonel.Stock.Services
{
    public interface IStockService
    {
        Task<Stock> GetStockByProductId(int productId);
        Task<List<Stock>> GetAllStock();
    }
}
