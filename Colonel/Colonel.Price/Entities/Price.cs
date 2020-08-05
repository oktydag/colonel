using Colonel.Price.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Colonel.Price
{
    public class Price : MongoBaseModel
    {
        [BsonElement("ProductId")]
        public string ProductId { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Value { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ReleaseDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime ExpireDate { get; set; }

        [BsonElement("IsActive")]
        public bool IsActive { get; set; }



   
    }
}
