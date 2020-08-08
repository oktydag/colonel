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


        public List<Price> GetAllPrices() =>
            _price.Find(price => true).ToList();

        public Price GetPriceByProductId(PriceRequestModel priceRequestModel)
        {
            return _price.Find<Price>(x =>
            x.ProductId == priceRequestModel.ProductId &&
            x.ReleaseDate <= priceRequestModel.RequestDate &&
            x.ExpireDate > priceRequestModel.RequestDate).FirstOrDefault();
        }
    }
}
