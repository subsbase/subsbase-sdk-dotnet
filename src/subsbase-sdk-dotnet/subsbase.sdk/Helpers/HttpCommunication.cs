using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace subsbase.sdk.Helpers
{
    internal class HttpCommunication : IHttpCommunication
    {
        private readonly HttpClient _httpClient;

        public HttpCommunication()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> SendAsync(HttpMethod methodType, string requestBody, string endpointUrl, string token, string siteId)
        {
            try
            {
                var request = new HttpRequestMessage()
                {
                    Method = methodType,
                    Content = new StringContent(requestBody, Encoding.UTF8, "application/json"),
                    RequestUri = new Uri(endpointUrl),
                    Headers =
                    {
                        { "x-site-id", siteId },
                        { "Authorization", $"Bearer {token}" },
                    }
                };

                var response = await _httpClient.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                
                // log that the request failed
                return string.Empty;
            }
            catch (Exception e)
            {
                // log error
                return String.Empty;
            }
        }
    }
}