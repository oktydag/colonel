using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colonel.User.Services
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int UserId);
        Task<List<User>> GetAllUsers();
        void InitializeData();
    }
}
