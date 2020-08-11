using Colonel.Price.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Price.Services
{
    public class PriceRepository : IPriceRepository
    {

        private readonly IMongoCollection<Price> _price;

        public PriceRepository(IPriceDatabaseSettings settings)
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

        public void InitializeData()
        {
            var priceList = new List<Price>()
            {
                new Price()
                {
                    ProductId = 321312333,
                    Value = 12.99m,
                    CampaignId = 122,
                    IsActive = true,
                    ReleaseDate = new DateTime(2020,02,26),
                    ExpireDate =new DateTime(2020,12,26)
                },
                new Price()
                {
                    ProductId = 15822066,
                    Value = 212.99m,
                    CampaignId = 32,
                    IsActive = false,
                    ReleaseDate = new DateTime(2020,02,26),
                    ExpireDate =new DateTime(2020,12,26)
                }
            };

            _price.InsertMany(priceList);
        }
    }
}
