using System;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json;
using subsbase.sdk.AuthorizationApi.Constants;
using subsbase.sdk.AuthorizationApi.Contracts.Requests;
using subsbase.sdk.AuthorizationApi.Contracts.Responses;
using subsbase.sdk.Helpers;

namespace subsbase.sdk.Authorization.Service
{
    public class AuthorizationApi : IAuthorizationApi
    {
        private readonly HttpCommunication _httpCommunication;
        private readonly GraphQLHttpClient _graphQlClient;
        
        public AuthorizationApi()
        {
            _httpCommunication = new HttpCommunication();
        }
        
        public async Task<string> GetAuthToken(string siteId, string apiSecret)
        {
            AuthRequest body =  new AuthRequest()
            {
                query = Constants.query, 
                operationName = Constants.operationName,
                variables = new AuthRequest.Variables()
                {
                    siteId = siteId,
                    apiSecret = apiSecret
                }
            };
            
            string serializedBody = JsonConvert.SerializeObject(body);
            var response = await _httpCommunication.SendAsync(HttpMethod.Post, serializedBody, Constants.apiUrl, string.Empty, siteId);

            if (response != String.Empty)
            {
                return JsonConvert.DeserializeObject<AuthResponse>(response).data.getApiToken.value;
            }
            
            return response;
        }
    }
}