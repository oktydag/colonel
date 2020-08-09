using Colonel.Shopping.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Colonel.Shopping.Entities
{
    public class Basket : MongoBaseModel
    {
        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("BasketLines")]
        public List<BasketLine> BasketLines { get; set; }

        [BsonElement("IsActive")]
        public bool IsActive { get; set; }

        [BsonElement("IsOrdered")]
        public bool IsOrdered { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdateDate { get; set; }
    }
}
