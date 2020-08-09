using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Product.Services
{
    public interface IProductService
    {
        Task<Product> GetProductById(int productId);
        Task<List<Product>> GetAllProducts();
    }
}
