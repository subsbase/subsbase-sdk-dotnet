using SubsBase.SDK.Authentication.Client;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Common.Contracts;
using SubsBase.SDK.Subscription.Builders.QueryBuilders;
using SubsBase.SDK.Subscription.Service;

namespace SubsBase.SDK.Subscription;

public class Queries
{
    private readonly SubscriptionService _subscriptionService;

    public Queries(SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    public CustomerQueryBuilder Customer(string customerId) => new CustomerQueryBuilder(_subscriptionService, customerId);
    public CustomersQueryBuilder Customers() => new CustomersQueryBuilder();
}