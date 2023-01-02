using System;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using subsbase.sdk.AuthorizationApi.Contracts.Responses;
using subsbase.sdk.Helpers;
using subsbase.sdk.SubscriptionApi.Contracts.Requests;
using subsbase.sdk.SubscriptionApi.Contracts.Responses;

namespace subsbase.sdk.SubscriptionApi.Service
{
    public class SubscriptionApi : ISubscriptionApi
    {
        private readonly HttpCommunication _httpCommunication;
        private readonly GraphQLHttpClient _graphQlClient;
        
        public SubscriptionApi()
        {
            _httpCommunication = new HttpCommunication();
            _graphQlClient = new GraphQLHttpClient(Constants.Constants.apiUrl, new NewtonsoftJsonSerializer());
        }

        public async Task<Customer> GetCustomerInfo(string siteId, string customerId, string authToken)
        {
            _graphQlClient.HttpClient.DefaultRequestHeaders.Add("x-site-id", "test-site");
            _graphQlClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");
            var request = new GraphQLRequest()
            {
                Query = Constants.Constants.query,
                Variables = new
                {
                    siteId = "test-site",
                    customerId = customerId
                }
            };
            var response = await _graphQlClient.SendQueryAsync<Response>(request);
            int x = 2;
            return new Customer();











            // Request body =  new Request()
            // {
            //     // needs to be only one Constants!!!!!!
            //     query = Constants.Constants.query,
            //     operationName = Constants.Constants.operationName,
            //     variables = new Request.Variables()
            //     {
            //         siteId = siteId,
            //         customerId = customerId
            //     }
            // };
            // string serializedBody = JsonConvert.SerializeObject(body);
            //
            // var response = await _httpCommunication.SendAsync(HttpMethod.Post, serializedBody, Constants.Constants.apiUrl, authToken, siteId);
            //
            // if (response != String.Empty)
            // {
            //     return JsonConvert.DeserializeObject<Response>(response).data.customer;
            // }
            //
            // // log that an error occured
            // return new Customer();
        }
    }
}