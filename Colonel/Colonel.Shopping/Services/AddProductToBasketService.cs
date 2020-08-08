﻿using Colonel.Shopping.Models;
using Colonel.Shopping.Models.Price;
using Colonel.Shopping.Models.Product;
using Colonel.Shopping.Models.Stock;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace Colonel.Shopping.Services
{
    public class AddProductToBasketService : IAddProductToBasketService
    {
        public StockResponseModel CheckProductHasStock(StockRequestModel stockRequestModel)
        {
            try
            {
                var client = new RestClient { BaseUrl = new Uri("http://localhost:58843/") };
                var request = new RestRequest("api/v1/stock/stockbyproductid", Method.GET);

                string requestModelAsJson = JsonConvert.SerializeObject(stockRequestModel, Formatting.Indented);
                request.AddParameter("application/json", requestModelAsJson, ParameterType.RequestBody);

                var response = client.Execute<StockResponseModel>(request);

                if (response.Data == null) return null; // TODO : 

                return response.Data;

            }
            catch (Exception ex)
            {

                throw ;
            }


        }

        public ProductResponseModel CheckProductOnSale(ProductRequestModel productRequestModel)
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
                var response = client.Execute<Models.ProductResponseModel>(request);


                //TODO : kontrol
                return response.Data;

            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public decimal GetProductPriceByDate(int productId, DateTime orderDate)
        {
            throw new NotImplementedException();
        }
    }
}
