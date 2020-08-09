using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colonel.Shopping.Models;
using Colonel.Shopping.Models.Stock;
using Newtonsoft.Json;
using RestSharp;

namespace Colonel.Shopping.Services
{
    public class StockService : IStockService
    {
        private readonly IProjectBaseUrlSettings _projectBaseUrlSettings;

        public StockService(IProjectBaseUrlSettings projectBaseUrlSettings)
        {
            _projectBaseUrlSettings = projectBaseUrlSettings;
        }
        public StockResponseModel HasAvailableStock(StockRequestModel stockRequestModel)
        {
            try
            {
                //TODO : ip adressleri ? 
                var client = new RestClient { BaseUrl = new Uri(_projectBaseUrlSettings.Stock) };
                var request = new RestRequest("api/v1/stocks", Method.GET);

                request.AddParameter("productId", stockRequestModel.ProductId, ParameterType.QueryString);
                request.AddParameter("quantity", stockRequestModel.Quantity, ParameterType.QueryString);

                var response = client.Execute<StockResponseModel>(request);

                return response.Data;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
