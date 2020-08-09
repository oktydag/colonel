using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colonel.Shopping.Models;
using Colonel.Shopping.Models.Product;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Colonel.Shopping.Services
{
    public class ProductService : IProductService
    {
        private readonly IProjectBaseUrlSettings _projectBaseUrlSettings;

        public ProductService(IProjectBaseUrlSettings projectBaseUrlSettings)
        {
            _projectBaseUrlSettings = projectBaseUrlSettings;
        }

        public ProductResponseModel GetProduct(ProductRequestModel productRequestModel)
        {
            try
            {
                var client = new RestClient { BaseUrl = new Uri(_projectBaseUrlSettings.Product) };
                var request = new RestRequest($"api/v1/products/{productRequestModel.ProductId}", Method.GET);

                var response = client.Execute<ProductResponseModel>(request);

                return response.Data;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
