using Colonel.Price.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colonel.Price.Services
{
    public interface IPriceServices
    {
        Task<Price> GetPriceByProductId(PriceRequestModel productId);
        Task<List<Price>> GetAllPrices();

    }
}
