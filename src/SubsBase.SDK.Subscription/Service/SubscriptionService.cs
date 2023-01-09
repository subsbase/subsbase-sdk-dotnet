using Newtonsoft.Json;
using SubsBase.SDK.Authentication.Client;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Common.Contracts;
using SubsBase.SDK.Subscription.Contracts;

namespace SubsBase.SDK.Subscription.Service;

public class SubscriptionService
{
    private readonly GraphQlClient _graphQlClient;
    private readonly SubsBaseSdkOptions _options;
    private readonly AuthenticationClient _authClient;

    public SubscriptionService(GraphQlClient graphQlClient,
        SubsBaseSdkOptions options,
        AuthenticationClient authClient)
    {
        _graphQlClient = graphQlClient;
        _options = options;
        _authClient = authClient;
    }

    public async Task<CustomerResponse?> GetCustomerInfoHelperAsync(string customerId, string selectedFields)
    {
        var token = await _authClient.ServerToken;
        string query = string.Format(Constants.CustomerQuery, selectedFields);
        var variables = new
        {
            siteId = _options.SiteId,
            customerId = customerId
        };

        var response = await _graphQlClient.SendAsync(Constants.CoreEndpoint, query, variables,
            Constants.CustomerOperationName, token);

        if (response == null)
        {
            return null;
        }

        var deserializedResponse = JsonConvert.DeserializeObject<CustomerResponse>(response.Data.ToString());
        return deserializedResponse;
    }
}