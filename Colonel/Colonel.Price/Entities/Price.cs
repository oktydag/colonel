using Colonel.Price.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Colonel.Price.Entities
{
    public class Price : MongoBaseModel
    {
        [BsonElement("ProductId")]
        public string ProductId { get; set; }

        [BsonElement("OnSale")]
        public bool OnSale { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ReleaseDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ExpireDate { get; set; }

        [BsonElement("IsActive")]
        public bool IsActive { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Value { get; set; }

   
    }
}
