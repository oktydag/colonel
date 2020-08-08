using Colonel.Shopping.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colonel.Shopping.Services
{
    public interface IUserService
    {
        UserResponseModel GetUser(UserRequestModel userRequestModel);
    }
}
