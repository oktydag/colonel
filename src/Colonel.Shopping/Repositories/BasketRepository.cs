using Colonel.Shopping.Entities;
using Colonel.Shopping.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Repositories
{
    public class BasketRepository: IBasketRepository
    {
        private readonly IMongoCollection<Basket> _basket;
        public BasketRepository(IBasketDatabaseSettings basketDatabaseSettings)
        {
            var basketClient = new MongoClient(basketDatabaseSettings.ConnectionString);
            var basketDatabase = basketClient.GetDatabase(basketDatabaseSettings.DatabaseName);

            _basket = basketDatabase.GetCollection<Basket>(basketDatabaseSettings.BasketCollectionName);
        }
        public Basket GetUserBasket(int userId)
        {
            return _basket.Find<Basket>(x => x.UserId == userId && x.IsActive == true && x.IsOrdered == false).FirstOrDefault();
        }

        public Basket SaveBasket(Basket basket)
        {
            if (basket == null) return null;

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
    }
}
