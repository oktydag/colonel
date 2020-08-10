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
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("{UserId}")]
        [Produces("application/json")]
        public async Task<ActionResult<User>> GetProductById([FromRoute] UserRequestModel userRequestModel)
        {
            //TODO : model is valid kontrolü
            var user = await _userRepository.GetUserById(userRequestModel.UserId);

            if (user == null)
                return NotFound($"The User whose id is equal to {userRequestModel.UserId} cannot be found.");

            if (!user.IsActive)
                return NotFound($"The User whose id is equal to {userRequestModel.UserId} is not active !");

            return user;
        }
    }
}