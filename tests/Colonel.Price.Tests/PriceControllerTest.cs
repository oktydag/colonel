using NUnit.Framework;
using Colonel.Price.Controllers;
using Moq;
using Colonel.Price.Services;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using Colonel.Price.Models;

namespace Colonel.Price.Tests
{
    [TestFixture]
    public class PriceControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task When_Price_Is_Given_It_Should_Be_Return_Ok()
        {
            Price price = new Price() { Id = "10101", ProductId = 5, Value = 10, CampaignId = 10,
                IsActive = true, ReleaseDate = new DateTime(2020,1,1), ExpireDate = new DateTime(2020, 12, 12)
            };

            var priceRepositoryMock = new Mock<IPriceRepository>();
            priceRepositoryMock.Setup(p => p.GetPriceByProductId(new PriceRequestModel() { ProductId = 5, RequestDate = DateTime.UtcNow})).Returns(Task.FromResult(price));

            var priceController = new PriceController(priceRepositoryMock.Object);
            var priceRequestModel = new PriceRequestModel() { ProductId = 5, RequestDate = new DateTime(2020, 5, 5) };

            var actionResult = await priceController.GetProductPrice(priceRequestModel);
            actionResult.Should().BeOfType<OkObjectResult>();

        }


        [Test]
        public async Task When_Price_Is_Given_Null_It_Should_Be_Return_NotFound()
        {
            Price nullPrice = null;

            var priceRepositoryMock = new Mock<IPriceRepository>();
            priceRepositoryMock.Setup(p => p.GetPriceByProductId(new PriceRequestModel() { ProductId = 5, RequestDate = DateTime.UtcNow })).Returns(Task.FromResult(nullPrice));

            var priceController = new PriceController(priceRepositoryMock.Object);
            var priceRequestModel = new PriceRequestModel() { ProductId = 5, RequestDate = new DateTime(2020, 5, 5) };

            var actionResult = await priceController.GetProductPrice(priceRequestModel);
            actionResult.Should().BeOfType<NotFoundObjectResult>();

        }
    }
}