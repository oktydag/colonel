using NUnit.Framework;
using Colonel.Product.Controllers;
using Moq;
using Colonel.Product.Services;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Colonel.Product.Models;

namespace Colonel.Product.Tests
{
    [TestFixture]
    class ProductControllerTest
    {

        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public async Task When_Product_Is_Given_It_Should_Be_Return_Ok()
        {
            Product Product = new Product() { Id = "10101", ProductId = 1000, ModelId
                = 101010, Name = "Oktay", OnSale = true};

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(p => p.GetProductById(1000)).Returns(Task.FromResult(Product));

            var ProductController = new ProductController(productRepositoryMock.Object);
            var ProductRequestModel = new ProductRequestModel() { ProductId = 1000 };

            var actionResult = await ProductController.GetProductById(ProductRequestModel);
            actionResult.Should().BeOfType<OkObjectResult>();

        }


        [Test]
        public async Task When_Product_Is_Given_Null_It_Should_Be_Return_NotFound()
        {
            Product nullProduct = null;

            var ProductRepositoryMock = new Mock<IProductRepository>();
            ProductRepositoryMock.Setup(p => p.GetProductById(1000)).Returns(Task.FromResult(nullProduct));

            var ProductController = new ProductController(ProductRepositoryMock.Object);
            var ProductRequestModel = new ProductRequestModel() { ProductId = 1000 };

            var actionResult = await ProductController.GetProductById(ProductRequestModel);
            actionResult.Should().BeOfType<NotFoundObjectResult>();

        }

        [Test]
        public async Task When_Product_Is_Not_Active_It_Should_Be_Return_NotFound()
        {
            Product Product = new Product()
            {
                Id = "10101",
                ProductId = 1000,
                ModelId
                = 101010,
                Name = "Oktay",
                OnSale = false
            };

            var ProductRepositoryMock = new Mock<IProductRepository>();
            ProductRepositoryMock.Setup(p => p.GetProductById(1000)).Returns(Task.FromResult(Product));

            var ProductController = new ProductController(ProductRepositoryMock.Object);
            var ProductRequestModel = new ProductRequestModel() { ProductId = 1000 };

            var actionResult = await ProductController.GetProductById(ProductRequestModel);
            actionResult.Should().BeOfType<NotFoundObjectResult>();

        }





    }
}
