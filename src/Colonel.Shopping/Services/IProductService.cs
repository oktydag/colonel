
using Colonel.Shopping.Models;
using Colonel.Shopping.Models.Product;

namespace Colonel.Shopping.Services
{
   public interface IProductService
    {
        ProductResponseModel GetProduct(ProductRequestModel productRequestModel);
    }
}
