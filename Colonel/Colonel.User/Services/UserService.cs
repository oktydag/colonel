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
        public async Task<User> GetUserById(int userId)
        {
            var user = await _user.Find<User>(x => x.UserId == userId).FirstOrDefaultAsync();

            return user;
        }

        public async Task<List<User>> GetAllUsers() =>
            await _user.Find(product => true).ToListAsync();
    }
}
