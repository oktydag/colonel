using Colonel.Price.Models;
using System.Collections.Generic;

namespace Colonel.Price.Services
{
    public interface IPriceServices
    {
        Price GetPriceByProductId(PriceRequestModel productId);
        List<Price> GetAllPrices();

    }
}
