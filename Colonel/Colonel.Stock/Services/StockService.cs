using Colonel.Stock.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Stock.Services
{
    public class StockService : IStockService
    {
        private readonly IMongoCollection<Stock> _stock;

        public StockService(IStockDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _stock = database.GetCollection<Stock>(settings.StockCollectionName);
        }


        public List<Stock> GetAllStock() =>
            _stock.Find(stock => true).ToList();

        public Stock GetStockByProductId(string productId) =>
            _stock.Find<Stock>(x => x.ProductId == productId).FirstOrDefault();
    }
}
