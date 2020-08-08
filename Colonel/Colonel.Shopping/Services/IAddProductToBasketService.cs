using Colonel.Shopping.Models;
using Colonel.Shopping.Models.Price;
using Colonel.Shopping.Models.Product;
using Colonel.Shopping.Models.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Services
{
    public interface IAddProductToBasketService
    {
        ProductResponseModel CheckProductOnSale(ProductRequestModel productRequestModel);
        StockResponseModel CheckProductHasStock( StockRequestModel stockRequestModel);
        decimal GetProductPriceByDate(int productId, DateTime orderDate);
    }
}
