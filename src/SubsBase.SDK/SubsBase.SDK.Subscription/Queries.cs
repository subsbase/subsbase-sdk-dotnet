using SubsBase.SDK.Authentication.Client;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Subscription.Builders.QueryBuilders;

namespace SubsBase.SDK.Subscription;

public class Queries
{
    private readonly GraphQLClient _graphQlClient;
    private readonly AuthenticationClient _authClient;

    public Queries(GraphQLClient graphQlClient, AuthenticationClient authClient)
    {
        _graphQlClient = graphQlClient;
        _authClient = authClient;
    }

    public CustomerQueryBuilder Customer(string customerId) => new CustomerQueryBuilder(_graphQlClient, customerId, _authClient);
    public CustomersQueryBuilder Customers() => new CustomersQueryBuilder();
}