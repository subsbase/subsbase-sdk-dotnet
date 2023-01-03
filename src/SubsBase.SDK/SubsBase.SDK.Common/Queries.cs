using SubsBase.SDK.Common.Builders.QueryBuilders;
using SubsBase.SDK.Common.Clients;

namespace SubsBase.SDK;

public class Queries
{
    private GraphQLClient _graphQlClient;

    public Queries(GraphQLClient graphQlClient)
    {
        _graphQlClient = graphQlClient;
    }

    public CustomerQueryBuilder Customer(string customerId) => new CustomerQueryBuilder(_graphQlClient, customerId);
    public CustomersQueryBuilder Customers() => new CustomersQueryBuilder();
}