using SubsBase.SDK.Common.Builders.MutationBuilders;
using SubsBase.SDK.Common.Clients;

namespace SubsBase.SDK;

public class Mutations
{
    private GraphQLClient _graphQlClient;

    public Mutations(GraphQLClient graphQlClient)
    {
        _graphQlClient = graphQlClient;
    }
    
    public CustomerMutationBuilder Customer(string customerId) => new CustomerMutationBuilder();
}