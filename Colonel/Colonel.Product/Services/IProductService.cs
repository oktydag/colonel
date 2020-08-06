using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Product.Services
{
    public interface IProductService
    {
        Product GetProductById(int productId);

        List<Product> GetAllProducts();
    }
}
