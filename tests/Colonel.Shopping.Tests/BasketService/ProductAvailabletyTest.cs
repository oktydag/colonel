using Colonel.Shopping.Models;
using Colonel.Shopping.Models.Price;
using Colonel.Shopping.Models.Product;
using Colonel.Shopping.Models.Stock;
using Colonel.Shopping.Models.User;
using Colonel.Shopping.Providers;
using Colonel.Shopping.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Colonel.Shopping.Tests.BasketService
{
    [TestFixture]
    class ProductAvailabletyTest
    {
        private readonly IProductService _productService;
        private readonly IPriceService _priceService;
        private readonly IStockService _stockService;
        private readonly IUserService _userService;

        public ProductAvailabletyTest(IProductService productService, IPriceService priceService, IStockService stockService, IUserService userService)
        {
            _productService = productService;
            _priceService = priceService;
            _stockService = stockService;
            _userService = userService;
        }
        AddProductToBasketRequestModel addProductToBasketRequestModel;

        [Test]
        public void Setup()
        {
           addProductToBasketRequestModel
                 = new AddProductToBasketRequestModel()
                 {
                     ProductId = 10,
                     GiftNote = "test",
                     UserId = 11,
                     Quantity = 12,
                     CreatedDate = new DateTime(2020, 3, 3)
                 };
        }

        public void When_Product_Is_Null_It_Should_Return_Product_Is_Not_Available(int productId, ProductAvailability expected)
        {
            //ProductResponseModel productResponseModel = null;
            //var productAvailability = CheckProductIsAvailable(addProductToBasketRequestModel);


            //var productRepositoryMock = new Mock<IProductService>();
            //productRepositoryMock.Setup(p => p.GetProduct(new Models.Product.ProductRequestModel() { ProductId = 1000})).Returns(productResponseModel);

            //var productResponse = _productService.GetProduct(new Models.Product.ProductRequestModel() { ProductId = 10 });


        }

        private ProductAvailability CheckProductIsAvailable(AddProductToBasketRequestModel basketItems)
        {
            var product = _productService.GetProduct(new ProductRequestModel() { ProductId = basketItems.ProductId });
            if (product == null)
                return ProductAvailability.NotAvailable;

            var user = _userService.GetUser(new UserRequestModel() { UserId = basketItems.UserId });
            if (user == null)
                return ProductAvailability.NotAvailable;

            var stockModelForRequestQuantity = _stockService.HasAvailableStock(new StockRequestModel() { ProductId = basketItems.ProductId, Quantity = basketItems.Quantity });
            if (stockModelForRequestQuantity == null)
                return ProductAvailability.NotAvailable;

            var priceOfProduct = _priceService.GetProductPrice(new PriceRequestModel()
            { ProductId = basketItems.ProductId, RequestDate = DateTimeProvider.Instance.GetUtcNow() });
            if (priceOfProduct == null)
                return ProductAvailability.NotAvailable;

            return new ProductAvailability(stockModelForRequestQuantity.Id, priceOfProduct.Value, basketItems.ProductId, true);
        }
    }
}
