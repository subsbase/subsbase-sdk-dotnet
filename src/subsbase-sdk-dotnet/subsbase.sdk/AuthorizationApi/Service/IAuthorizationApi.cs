using System.Threading.Tasks;

namespace subsbase.sdk.Authorization.Service
{
    public interface IAuthorizationApi
    {
        Task<string> GetAuthToken(string siteId, string apiSecret);
    }
}