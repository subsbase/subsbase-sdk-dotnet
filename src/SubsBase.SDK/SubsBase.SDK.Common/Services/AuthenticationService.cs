using Newtonsoft.Json;
using SubsBase.SDK.Common.Contracts.Responses;

namespace SubsBase.SDK.Common.Services;

public class AuthenticationService
{
    private readonly GraphQLClient _graphQLClient;

    public AuthenticationService(GraphQLClient graphQLClient)
    {
        _graphQLClient = graphQLClient;
    }
    
    public async Task<string> GenerateToken(string siteId, string apiSecret)
    {
        string query = Constants.authQuery;
        var variables = new
        {
            siteId = siteId,
            apiSecret =apiSecret
        };
        
        var response = await _graphQLClient.SendAsync(Constants.authEndpoint, query, variables, Constants.authOperationName, string.Empty);
        AuthResponse deserializedResponse = JsonConvert.DeserializeObject<AuthResponse>(response.Data.ToString());

        return deserializedResponse.getApiToken.value;
    }
}