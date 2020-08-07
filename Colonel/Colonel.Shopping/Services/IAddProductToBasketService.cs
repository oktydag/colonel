using Colonel.Shopping.Models.Price;
using Colonel.Shopping.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Services
{
    public interface IAddProductToBasketService
    {
        bool CheckProductOnSale(ProductRequestModel productId);
        bool CheckProductHasStock(PriceRequestModel productId);
        decimal GetProductPriceByDate(int productId, DateTime orderDate);
    }
}
