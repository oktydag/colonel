using Colonel.User.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Colonel.User
{
    public class User : MongoBaseModel
    {
        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Surname")]
        public string Surname { get; set; }

        [BsonElement("PhoneNumber")]
        public long PhoneNumber { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("IsActive")]
        public bool IsActive { get; set; }
    }
}
