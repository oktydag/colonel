using System.Collections.Generic;

namespace Colonel.Price.Services
{
    public interface IPriceServices
    {
        Price GetPriceByProductId(string productId);
        List<Price> GetAllPrices();

    }
}
