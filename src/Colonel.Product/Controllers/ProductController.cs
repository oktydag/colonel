using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colonel.Product.Models;
using Colonel.Product.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Product.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("{ProductId}")]
        [Produces("application/json")]
        public async Task<ActionResult<Product>> GetProductById([FromRoute] ProductRequestModel productRequestModel) {

            var product = await _productRepository.GetProductById(productRequestModel.ProductId);

            if (product == null)
                return NotFound($"The Product whose id is equal to {productRequestModel.ProductId} cannot be found.");

            if (!product.OnSale)
                return NotFound($"The Product whose id is equal to {productRequestModel.ProductId} is not on sale !");

            return product;
        }


        [HttpGet]
        [Route("List")]
        [Produces("application/json")]
        public async Task<ActionResult<List<Product>>> Get() =>
         await _productRepository.GetAllProducts();
    }
}