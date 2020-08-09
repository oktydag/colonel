using Colonel.Price.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Price.Services
{
    public class PriceServices : IPriceServices
    {

        private readonly IMongoCollection<Price> _price;

        public PriceServices(IPriceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _price = database.GetCollection<Price>(settings.PriceCollectionName);
        }


        public async Task<List<Price>> GetAllPrices() =>
            await _price.Find(price => true).ToListAsync();

        public async Task<Price> GetPriceByProductId(PriceRequestModel priceRequestModel)
        {
            return  await _price.Find<Price>(x =>
            x.ProductId == priceRequestModel.ProductId &&
            x.ReleaseDate <= priceRequestModel.RequestDate &&
            x.ExpireDate > priceRequestModel.RequestDate).FirstOrDefaultAsync();
        }
    }
}
