using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colonel.Product.Services
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int productId);
        Task<List<Product>> GetAllProducts();
    }
}
