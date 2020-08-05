using Colonel.Stock.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Stock.Services
{
    public class StockService
    {
        private readonly IMongoCollection<Stock> _stock;

        public StockService(IStockDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _stock = database.GetCollection<Stock>(settings.StockCollectionName);
        }


        public List<Stock> Get() =>
            _stock.Find(book => true).ToList();

        public Stock Get(string id) =>
            _stock.Find<Stock>(book => book.Id == id).FirstOrDefault();
    }
}
