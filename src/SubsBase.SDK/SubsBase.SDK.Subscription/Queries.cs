using SubsBase.SDK.Authentication.Client;
using SubsBase.SDK.Common;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Subscription.Builders.QueryBuilders;

namespace SubsBase.SDK.Subscription;

public class Queries
{
    private readonly GraphQLClient _graphQlClient;
    private readonly AuthenticationClient _authClient;
    private readonly SubsBaseSdkOptions _options;

    public Queries(GraphQLClient graphQlClient, AuthenticationClient authClient, SubsBaseSdkOptions options)
    {
        _graphQlClient = graphQlClient;
        _authClient = authClient;
        _options = options;
    }

    public CustomerQueryBuilder Customer(string customerId) => new CustomerQueryBuilder(_graphQlClient, customerId, _authClient, _options);
    public CustomersQueryBuilder Customers() => new CustomersQueryBuilder();
}