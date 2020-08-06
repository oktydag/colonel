using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colonel.Product.Services;
using Microsoft.AspNetCore.Mvc;

namespace Colonel.Product.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        [Route("productbyid")]
        public ActionResult<Product> GetProductById(int productId) {
            var product = _productService.GetProductById(productId);

            if (product == null) return NotFound($"The Product whose id is equal to {productId} cannot be found.");
            return product;
        }
           


        [HttpGet]
        [Route("allproducts")]
        public ActionResult<List<Product>> Get() =>
         _productService.GetAllProducts();
    }
}