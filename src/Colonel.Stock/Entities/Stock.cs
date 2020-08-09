using Colonel.Stock.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Colonel.Stock
{
    public class Stock: MongoBaseModel
    {
        [BsonElement("ProductId")]
        public int ProductId { get; set; }

        [BsonElement("Value")]
        public int Value { get; set; }
    }
}
