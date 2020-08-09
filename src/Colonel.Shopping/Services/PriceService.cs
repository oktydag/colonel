using System;
using Colonel.Shopping.Models;
using Colonel.Shopping.Models.Price;
using Newtonsoft.Json;
using RestSharp;

namespace Colonel.Shopping.Services
{
    public class PriceService : IPriceService
    {
        private readonly IProjectBaseUrlSettings _projectBaseUrlSettings;

        public PriceService(IProjectBaseUrlSettings projectBaseUrlSettings)
        {
            _projectBaseUrlSettings = projectBaseUrlSettings;
        }
        public PriceResponseModel GetProductPrice(PriceRequestModel priceRequestModel)
        {
            try
            {
                var client = new RestClient { BaseUrl = new Uri(_projectBaseUrlSettings.Price) };
                var request = new RestRequest("api/v1/prices", Method.GET);

                request.AddParameter("productId", priceRequestModel.ProductId, ParameterType.QueryString);
                request.AddParameter("requestDate", priceRequestModel.RequestDate, ParameterType.QueryString);

                var response = client.Execute<PriceResponseModel>(request);

                return response.Data;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
