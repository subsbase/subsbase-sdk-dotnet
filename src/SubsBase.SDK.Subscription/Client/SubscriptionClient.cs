using SubsBase.SDK.Subscription.Contracts;
using SubsBase.SDK.Subscription.Service;

namespace SubsBase.SDK.Subscription.Client;

public class SubscriptionClient
{
    private readonly SubscriptionService _subscriptionService;

    public SubscriptionClient(SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    public async Task<CustomerResponse> GetCustomerInfoAsync(string customerId, string selectedFields)
    {
        var response = await _subscriptionService.GetCustomerInfoHelperAsync(customerId, selectedFields);

        return response;
    }
}