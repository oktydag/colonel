using Colonel.Shopping.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Colonel.Shopping.Entities
{
    // TODO : BasketItem mı olmalı ? 
    public class BasketLine : MongoBaseModel
    {
        [BsonElement("ProductId")]
        public int ProductId { get; set; }

        [BsonElement("CustomerId")]
        public int CustomerId { get; set; }

        [BsonElement("Quantity")]
        public int Quantity { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdateDate { get; set; }
    }
}
