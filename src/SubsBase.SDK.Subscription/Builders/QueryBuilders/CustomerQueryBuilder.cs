using System.Linq.Expressions;
using SubsBase.SDK.Common.Contracts;
using SubsBase.SDK.Common.Selectors;
using SubsBase.SDK.Common.Utils;
using SubsBase.SDK.Subscription.Client;
using SubsBase.SDK.Subscription.Contracts;

namespace SubsBase.SDK.Subscription.Builders.QueryBuilders;

public class CustomerQueryBuilder : IFieldSelector<Customer, CustomerQueryBuilder>
{
    private readonly SubscriptionClient _subscriptionClient;
    private readonly string _customerId;
    private string _selectedFields = string.Empty;


    internal CustomerQueryBuilder(SubscriptionClient subscriptionClient, string customerId)
    {
        _subscriptionClient = subscriptionClient;
        _customerId = customerId;
    }

    public CustomerQueryBuilder Select<T>(Expression<Func<Customer, T>> selector)
    {
        _selectedFields = FieldSelectorBuilder.Parse(selector);
        return this;
    }


    public async Task<Result<CustomerResponse>> ExecuteAsync()
    {
        var customerResponse = await _subscriptionClient.GetCustomerInfoAsync(_customerId, _selectedFields);
        return new Result<CustomerResponse>(customerResponse);
    }
}