using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.User.Services
{
    public interface IUserService
    {
        User GetUserById(int UserId);
        List<User> GetAllUsers();
    }
}
