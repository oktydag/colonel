﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Colonel.Price.Models
{
    public abstract class MongoBaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
