using Colonel.Shopping.Entities;
using Colonel.Shopping.Models;
using Colonel.Shopping.Providers;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Colonel.Shopping.Services
{
    public class BasketService : IBasketService
    {
        private readonly IMongoCollection<BasketLine> _basketLine;
        private readonly IMongoCollection<Basket> _basket;
        public BasketService(IBasketLineDatabaseSettings basketLineSettings, IBasketDatabaseSettings basketDatabaseSettings)
        {
            var basketLineClient = new MongoClient(basketLineSettings.ConnectionString);
            var basketlineDatabase = basketLineClient.GetDatabase(basketLineSettings.DatabaseName);

            _basketLine = basketlineDatabase.GetCollection<BasketLine>(basketLineSettings.BasketLineCollectionName);

            var basketClient = new MongoClient(basketDatabaseSettings.ConnectionString);
            var basketDatabase = basketLineClient.GetDatabase(basketDatabaseSettings.DatabaseName);

            _basket = basketlineDatabase.GetCollection<Basket>(basketDatabaseSettings.BasketCollectionName);

        }

        public BasketLine AddBasketLine(BasketLine basketLine)
        {
            if (basketLine == null) return null;
            _basketLine.InsertOne(basketLine);

            return basketLine;
        }

        public bool AddItemsToBasket(BasketLine basketLine, Basket basket)
        {
            _basketLine.InsertOne(basketLine);
            _basket.InsertOne(basket);

            return true;
        }

        public Basket SaveBasket(Basket basket)
        {
            if (basket == null) return null;
            //var filter = Builders<Basket>.Filter.Eq("_id", basket.Id);
            //var update = Builders<Basket>.Update;

            var filterBuilder = Builders<Basket>.Filter;
            var filter = filterBuilder.Where(x => x.UserId == basket.UserId);
            var updateBuilder = Builders<Basket>.Update;

            var update = updateBuilder
                .Set(f => f.BasketLines, basket.BasketLines)
                .Set(f => f.UpdateDate, basket.UpdateDate)
                .Set(f => f.UserId, basket.UserId)
                .Set(f => f.IsActive, basket.IsActive)
                .Set(f => f.IsOrdered, basket.IsOrdered)
                .Set(f => f.CreatedDate, basket.CreatedDate
                );

            _basket.UpdateOne(filter, update, new UpdateOptions() { IsUpsert = true });

            return basket;
        }

        public Basket GetUserBasket(int userId)
        {
            return _basket.Find<Basket>(x => x.UserId == userId && x.IsActive == true && x.IsOrdered == false).FirstOrDefault();
        }

    }
}
