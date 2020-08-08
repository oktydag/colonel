using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Entities
{
    public class Basket
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
