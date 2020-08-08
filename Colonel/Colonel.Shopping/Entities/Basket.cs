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

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? CreatedDate { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdateDate { get; set; }
    }
}
