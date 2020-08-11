using Colonel.Product.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Product.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _product;

        public ProductRepository(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _product = database.GetCollection<Product>(settings.ProductCollectionName);
        }

        public async Task<List<Product>> GetAllProducts() =>
           await _product.Find(product => true).ToListAsync();

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _product.Find<Product>(x => x.ProductId == productId).FirstOrDefaultAsync();

            return product;
        }

        public void InitializeData()
        {
            var productList = new List<Product>()
            {
                new Product()
                {
                    ProductId = 321312333,
                    OnSale = false,
                    ModelId = 213,
                    Name = "Pantalon"
                },
               new Product()
                {
                    ProductId = 15822066,
                    OnSale = true,
                    ModelId = 10,
                    Name = "Cicek"
                }
            };

            _product.InsertMany(productList);
        }

    }
}
