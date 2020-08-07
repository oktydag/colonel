using Colonel.Shopping.Models.Price;
using Colonel.Shopping.Models.Product;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Colonel.Shopping.Services
{
    public class AddProductToBasketService : IAddProductToBasketService
    {
        public bool CheckProductHasStock(PriceRequestModel priceRequestModel)
        {
            //try
            //{
            //    var client = new RestClient { BaseUrl = new Uri("http://localhost:58843/") };
            //    var request = new RestRequest("api/v1/product/productbyid", Method.GET);

            //    string requestModelAsJson = JsonConvert.SerializeObject(priceRequestModel, Formatting.Indented);
            //    request.AddParameter("application/json", requestModelAsJson, ParameterType.RequestBody);

            //    var response = client.Execute<PriceResponseModel>(request);
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}


            return true;
        }

        public bool CheckProductOnSale(ProductRequestModel productRequestModel)
        {
            try
            {
                var client = new RestClient { BaseUrl = new Uri("http://localhost:58843/") };
                var request = new RestRequest("api/v1/product/productbyid", Method.GET);

                //request.AddHeader("Content-Type", "application/json");
                //string serializedBody = JsonConvert.SerializeObject(productRequestModel);
                //request.AddParameter("application/json; charset=utf-8", serializedBody, ParameterType.RequestBody);

                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(productRequestModel);
                var response = client.Execute<ProductResponseModel>(request);


                return true;
            }
            catch (Exception ex)
            {

                throw;
            }


            return true;
        }

        public decimal GetProductPriceByDate(int productId, DateTime orderDate)
        {
            throw new NotImplementedException();
        }
    }
}
