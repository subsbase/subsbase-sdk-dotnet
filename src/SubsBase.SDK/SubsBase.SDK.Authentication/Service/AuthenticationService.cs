using Newtonsoft.Json;
using SubsBase.SDK.Common.Clients;

namespace SubsBase.SDK.Authentication.Service;

public class AuthenticationService
{
    private readonly GraphQLClient _graphQLClient;

    public AuthenticationService(GraphQLClient graphQLClient)
    {
        _graphQLClient = graphQLClient;
    }
    
    public async Task<string> GenerateToken(string siteId, string apiSecret)
    {
        string query = Constants.AuthQuery;
        var variables = new
        {
            siteId = siteId,
            apiSecret =apiSecret
        };
        
        var response = await _graphQLClient.SendAsync(Constants.AuthEndpoint, query, variables, Constants.AuthOperationName, string.Empty);
        AuthResponse deserializedResponse = JsonConvert.DeserializeObject<AuthResponse>(response.Data.ToString());

        return deserializedResponse.getApiToken.value;
    }
}