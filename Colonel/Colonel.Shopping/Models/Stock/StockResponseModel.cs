using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Colonel.Shopping.Models.Stock
{
    public class StockResponseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int ProductId { get; set; }

        public int Value { get; set; }
    }
}
