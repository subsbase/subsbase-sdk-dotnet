using System.Linq.Expressions;
using Newtonsoft.Json;
using SubsBase.SDK.Authentication.Client;
using SubsBase.SDK.Authentication.Service;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Common.Contracts;
using SubsBase.SDK.Common.Selectors;
using SubsBase.SDK.Common.Utils;
using SubsBase.SDK.Subscription.Contracts;
using SubsBase.SDK.Subscription.Service;

namespace SubsBase.SDK.Subscription.Builders.QueryBuilders;

public class CustomerQueryBuilder : IFieldSelector<Customer, CustomerQueryBuilder>
{
    private readonly string _customerId;
    private readonly SubscriptionService _subscriptionService;
    private string _selectedFields = string.Empty;


    internal CustomerQueryBuilder(SubscriptionService subscriptionService, string customerId)
    {
        _subscriptionService = subscriptionService;
        _customerId = customerId;
    }
    
    public CustomerQueryBuilder Select<T>(Expression<Func<Customer, T>> selector)
    {
        _selectedFields = FieldSelectorBuilder.Parse(selector);
        return this;
    }


    public async Task<Result<CustomerResponse>> ExecuteAsync()
    {
        var customerResponse = await _subscriptionService.GetCustomerInfoAsync(_customerId, _selectedFields);
        return new Result<CustomerResponse>(customerResponse);
    }
}