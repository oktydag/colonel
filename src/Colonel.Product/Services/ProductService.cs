﻿using Colonel.Product.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<List<Product>> GetAllProducts() =>
           await _product.Find(product => true).ToListAsync();

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _product.Find<Product>(x => x.ProductId == productId).FirstOrDefaultAsync();

            return product;
        }

    }
}