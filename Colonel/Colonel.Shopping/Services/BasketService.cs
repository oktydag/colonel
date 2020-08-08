using Colonel.Shopping.Entities;
using Colonel.Shopping.Models;
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

        public Basket AddBasket(Basket basket)
        {
            if (basket == null) return null;
            _basket.InsertOne(basket);

            return basket;
          
        }

        public Basket GetUserBasket(int userId)
        {
           return _basket.Find<Basket>(x => x.UserId == userId).FirstOrDefault();
        }

       public bool IncreaseQuantityOfProductInBasket(int userId , int newQuantity)
        {
            var filter = Builders<Basket>.Filter.Eq("UserId", userId);


            // TODO: update filter ? does not work correctly !
            var update = Builders<Basket>.Update;
            var quantityInBasketSetter = update.Set("BasketLines.$.Quantity", newQuantity);
            _basket.UpdateOne(filter, quantityInBasketSetter);


            return true;
        }
    }
}
