using Colonel.Shopping.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Colonel.Shopping.Entities
{
    public class BasketLine : MongoBaseModel
    {
        [BsonElement("ProductId")]
        public int ProductId { get; set; }

        [BsonElement("Quantity")]
        public int Quantity { get; set; }

        [BsonElement("StockId")]
        public string StockId { get; set; }

        [BsonElement("GiftNote")]
        public string GiftNote { get; set; }
    }
}
