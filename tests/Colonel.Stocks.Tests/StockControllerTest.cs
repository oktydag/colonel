using NUnit.Framework;
using Colonel.Stock.Controllers;
using Moq;
using Colonel.Stock.Services;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Stocks.Tests
{
    [TestFixture]
    class StockControllerTest
    {
        
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task When_Stock_Is_Given_It_Should_Be_Return_Ok()
        {
            Stock.Stock stock = new Stock.Stock() { Id = "10101", ProductId = 10, Value = 10 };

            var stockRepositoryMock = new Mock<IStockRepository>();
            stockRepositoryMock.Setup(p => p.GetStockByProductId(10)).Returns(Task.FromResult(stock));

            var stockController = new StockController(stockRepositoryMock.Object);
            var stockRequestModel = new Stock.Models.StockRequestModel() { ProductId = 10, Quantity = 5 };

            var actionResult = await stockController.GetProductStockCount(stockRequestModel);
            actionResult.Should().BeOfType<OkObjectResult>();

        }


        [Test]
        public async Task When_Stock_Is_Given_Null_It_Should_Be_Return_NotFound()
        {
            Stock.Stock nullStock =  null;

            var stockRepositoryMock = new Mock<IStockRepository>();
            stockRepositoryMock.Setup(p => p.GetStockByProductId(10)).Returns(Task.FromResult(nullStock));

            var stockController = new StockController(stockRepositoryMock.Object);
            var stockRequestModel = new Stock.Models.StockRequestModel() { ProductId = 10, Quantity = 4 };

            var actionResult = await stockController.GetProductStockCount(stockRequestModel);
            actionResult.Should().BeOfType<NotFoundObjectResult>();
            
        }

        [Test]
        public async Task When_Quantity_Is_Given_Larger_It_Should_Be_Return_NotFound()
        {
            Stock.Stock stock = new Stock.Stock() {Id = "10101", ProductId = 10, Value = 10 };

            var stockRepositoryMock = new Mock<IStockRepository>();
            stockRepositoryMock.Setup(p => p.GetStockByProductId(10)).Returns(Task.FromResult(stock));

            var stockController = new StockController(stockRepositoryMock.Object);

            var stockRequestModel = new Stock.Models.StockRequestModel() { ProductId = 10, Quantity = 40 };

            var actionResult = await stockController.GetProductStockCount(stockRequestModel);
            actionResult.Should().BeOfType<NotFoundObjectResult>();

        }





    }
}
