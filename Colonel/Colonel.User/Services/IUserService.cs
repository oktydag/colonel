using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.User.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(int UserId);
        Task<List<User>> GetAllUsers();
    }
}
