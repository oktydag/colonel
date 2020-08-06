using System.Collections.Generic;

namespace Colonel.Price.Services
{
    public interface IPriceServices
    {
        Price GetPriceByProductId(int productId);
        List<Price> GetAllPrices();

    }
}
