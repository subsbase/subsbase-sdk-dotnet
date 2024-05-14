using SubsBase.SDK.Subscription.Builders.QueryBuilders;
using SubsBase.SDK.Subscription.Client;

namespace SubsBase.SDK.Subscription;

public class Queries
{
    private readonly SubscriptionClient _subscriptionClient;

    public Queries(SubscriptionClient subscriptionClient)
    {
        _subscriptionClient = subscriptionClient;
    }

    public CustomerQueryBuilder Customer(string customerId) =>
        new CustomerQueryBuilder(_subscriptionClient, customerId);

    public CustomersQueryBuilder Customers() => new CustomersQueryBuilder();
}