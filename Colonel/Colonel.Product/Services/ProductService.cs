using Colonel.Product.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Colonel.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _product;

        public ProductService(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _product = database.GetCollection<Product>(settings.ProductCollectionName);
        }

        public List<Product> GetAllProducts() =>
            _product.Find(product => true).ToList();

        public Product GetProductById(int productId) =>
            _product.Find<Product>(x => x.ProductId == productId).FirstOrDefault();
    }
}
