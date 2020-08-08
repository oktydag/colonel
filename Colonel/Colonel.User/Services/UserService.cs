using Colonel.User.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.User.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _user;
        public UserService(IUserDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<User>(settings.UserCollectionName);
        }
        public User GetUserById(int userId)
        {
            var user = _user.Find<User>(x => x.UserId == userId).FirstOrDefault();

            return user;
        }

        public List<User> GetAllUsers() =>
            _user.Find(product => true).ToList();
    }
}
