using System.Threading.Tasks;
using Colonel.User.Models;
using Colonel.User.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.User.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("{UserId}")]
        [Produces("application/json")]
        public async Task<ActionResult<User>> GetProductById([FromRoute] UserRequestModel userRequestModel)
        {
            //TODO : model is valid kontrolü
            var user = await _userService.GetUserById(userRequestModel.UserId);

            if (user == null)
                return NotFound($"The User whose id is equal to {userRequestModel.UserId} cannot be found.");

            if (!user.IsActive)
                return NotFound($"The User whose id is equal to {userRequestModel.UserId} is not active !");

            return user;
        }
    }
}