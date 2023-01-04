using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Subscription.Builders.MutationBuilders;

namespace SubsBase.SDK.Subscription;

public class Mutations
{
    private GraphQLClient _graphQlClient;

    public Mutations(GraphQLClient graphQlClient)
    {
        _graphQlClient = graphQlClient;
    }
    
    public CustomerMutationBuilder Customer(string customerId) => new CustomerMutationBuilder();
}