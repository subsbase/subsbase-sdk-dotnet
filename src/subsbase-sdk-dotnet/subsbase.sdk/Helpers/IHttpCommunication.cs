using System.Net.Http;
using System.Threading.Tasks;

namespace subsbase.sdk.Helpers
{
    public interface IHttpCommunication
    {
        Task<string> SendAsync(HttpMethod methodType, string requestBody, string endpointUrl, string token, string siteId);
    }
}