using NUnit.Framework;
using Colonel.Price.Controllers;
using Moq;
using Colonel.Price.Services;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Price.Tests
{
    [TestFixture]
    public class PriceControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        //public async Task When_Stock_Is_Given_It_Should_Be_Return_Ok()
        //{
        //    Price price = new Price() { Id = "10101", ProductId = 10, Value = 10 };

        //    var stockRepositoryMock = new Mock<IPriceRepository>();
        //    stockRepositoryMock.Setup(p => p.GetStockByProductId(10)).Returns(Task.FromResult(price));

        //    var stockController = new StockController(stockRepositoryMock.Object);
        //    var stockRequestModel = new Stock.Models.StockRequestModel() { ProductId = 10, Quantity = 5 };

        //    var actionResult = await stockController.GetProductStockCount(stockRequestModel);
        //    actionResult.Should().BeOfType<OkObjectResult>();

        //}
    }
}