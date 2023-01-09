using Newtonsoft.Json;
using SubsBase.SDK.Authentication.Contracts;
using SubsBase.SDK.Common.Clients;

namespace SubsBase.SDK.Authentication.Service;

public class AuthenticationService
{
    private readonly GraphQlClient _graphQlClient;

    internal AuthenticationService(GraphQlClient graphQlClient)
    {
        _graphQlClient = graphQlClient;
    }

    public async Task<string> GenerateTokenAsync(string siteId, string apiSecret)
    {
        var variables = new
        {
            siteId = siteId,
            apiSecret = apiSecret
        };

        var response = await _graphQlClient.SendAsync(Constants.AuthEndpoint, Constants.AuthQuery, variables,
            Constants.AuthOperationName, string.Empty);

        if (response == null)
        {
            return string.Empty;
        }

        var deserializedResponse = JsonConvert.DeserializeObject<AuthResponse>(response.Data.ToString());
        return deserializedResponse?.GetApiToken.Value;
    }
}