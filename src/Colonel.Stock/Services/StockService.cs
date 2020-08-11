using Colonel.Stock.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colonel.Stock.Services
{
    public class StockRepository : IStockRepository
    {
        private readonly IMongoCollection<Stock> _stock;

        public StockRepository(IStockDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _stock = database.GetCollection<Stock>(settings.StockCollectionName);
        }


        public async Task<List<Stock>> GetAllStock() =>
            await _stock.Find(stock => true).ToListAsync();

        public async Task<Stock> GetStockByProductId(int productId) =>
           await _stock.Find<Stock>(x => x.ProductId == productId).FirstOrDefaultAsync();

        public void InitializeData()
        {
            var stockList = new List<Stock>()
            {
                new Stock()
                {
                    ProductId = 321312333,
                    Value = 20,
                },
                new Stock()
                {
                    ProductId = 15822066,
                    Value = 10,
                }
            };

            _stock.InsertMany(stockList);
        }
    }
}
