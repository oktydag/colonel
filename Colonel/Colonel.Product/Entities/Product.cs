using Colonel.Product.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Colonel.Product
{
    public class Product : MongoBaseModel
    {
        [BsonElement("ProductId")]
        public int ProductId { get; set; }

        [BsonElement("ModelId")]
        public int ModelId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("OnSale")]
        public bool OnSale { get; set; }
    }
}
