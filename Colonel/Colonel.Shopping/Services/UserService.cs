using Colonel.Shopping.Models;
using Colonel.Shopping.Models.User;
using RestSharp;
using System;

namespace Colonel.Shopping.Services
{
    public class UserService: IUserService
    {
        private readonly IProjectBaseUrlSettings _projectBaseUrlSettings;

        public UserService(IProjectBaseUrlSettings projectBaseUrlSettings)
        {
            _projectBaseUrlSettings = projectBaseUrlSettings;
        }

        public UserResponseModel GetUser(UserRequestModel userRequestModel)
        {
            try
            {
                var client = new RestClient { BaseUrl = new Uri(_projectBaseUrlSettings.User) };
                var request = new RestRequest($"api/v1/users/{userRequestModel.UserId}", Method.GET);

                var response = client.Execute<UserResponseModel>(request);

                return response.Data;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
