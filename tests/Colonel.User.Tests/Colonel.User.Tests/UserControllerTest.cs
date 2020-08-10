using NUnit.Framework;
using Colonel.User.Controllers;
using Moq;
using Colonel.User.Services;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Colonel.User.Models;

namespace Colonel.User.Tests
{
    [TestFixture]
    class UserControllerTest
    {

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task When_User_Is_Given_It_Should_Be_Return_Ok()
        {
            User user = new User() { Id = "10101", UserId = 1000, PhoneNumber = 101010, Name = "Oktay", Surname = "Dagdelen", IsActive = true };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(p => p.GetUserById(1000)).Returns(Task.FromResult(user));

            var userController = new UserController(userRepositoryMock.Object);
            var userRequestModel = new UserRequestModel() { UserId = 1000 };

            var actionResult = await userController.GetProductById(userRequestModel);
            actionResult.Should().BeOfType<OkObjectResult>();

        }


        [Test]
        public async Task When_User_Is_Given_Null_It_Should_Be_Return_NotFound()
        {
            User nullUser = null;

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(p => p.GetUserById(1000)).Returns(Task.FromResult(nullUser));

            var userController = new UserController(userRepositoryMock.Object);
            var userRequestModel = new UserRequestModel() { UserId = 1000 };

            var actionResult = await userController.GetProductById(userRequestModel);
            actionResult.Should().BeOfType<NotFoundObjectResult>();

        }

        [Test]
        public async Task When_User_Is_Not_Active_It_Should_Be_Return_NotFound()
        {
            User user = new User() { Id = "10101", UserId = 1000, PhoneNumber = 101010, Name = "Oktay", Surname = "Dagdelen", IsActive = false };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(p => p.GetUserById(1000)).Returns(Task.FromResult(user));

            var userController = new UserController(userRepositoryMock.Object);
            var userRequestModel = new UserRequestModel() { UserId = 1000 };

            var actionResult = await userController.GetProductById(userRequestModel);
            actionResult.Should().BeOfType<NotFoundObjectResult>();

        }





    }
}
