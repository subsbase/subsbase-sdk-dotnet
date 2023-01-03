using SubsBase.SDK.Common.Clients;

namespace SubsBase.SDK.Subscription;

public class SubscriptionService
{
    private readonly GraphQLClient _graphQLClient;

    public SubscriptionService(GraphQLClient graphQLClient)
    {
        _graphQLClient = graphQLClient;
    }
}