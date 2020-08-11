using Colonel.User.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.User.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _user;
        public UserRepository(IUserDatabaseSettings settings)
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

        public void InitializeData()
        {
            var userList = new List<User>()
            {
                new User()
                {
                    UserId = 54321,
                    Name = "Oktay",
                    Surname = "Dagdelen",
                    IsActive = true,
                    PhoneNumber = 55443322
                },
                new User()
                {
                    UserId = 4321,
                    Name = "Veli",
                    Surname = "Dagdelen",
                    IsActive = true,
                    PhoneNumber = 442211231
                }
            };

            _user.InsertMany(userList);
        }
    }
}
