using System.Linq.Expressions;
using SubsBase.SDK.Authentication.Client;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Common.Contracts;
using SubsBase.SDK.Common.Selectors;
using SubsBase.SDK.Common.Utils;

namespace SubsBase.SDK.Subscription.Builders.QueryBuilders;

public class CustomerQueryBuilder : IFieldSelector<Customer, CustomerQueryBuilder>
{
    private readonly GraphQLClient _gqlClient;
    private readonly string _customerId;
    private readonly AuthenticationClient _authClient;
    private string selectedFields = string.Empty;


    internal CustomerQueryBuilder(GraphQLClient gqlClient, string customerId, AuthenticationClient authClient)
    {
        _gqlClient = gqlClient;
        _customerId = customerId;
        _authClient = authClient;
    }
    
    public CustomerQueryBuilder Select<T>(Expression<Func<Customer, T>> selector)
    {
        //extract and save selections
        selectedFields = FieldSelectorBuilder.Parse(selector);
        return this;
    }


    public async Task<Result<Customer>> ExecuteAsync()
    {
        Customer customer = new Customer();
        GetCustomerInfo();
        return await Task.FromResult(new Result<Customer>(new Customer())); //with actual fields and actually
    }

    private async Task GetCustomerInfo()
    {
        var token = await _authClient.ServerToken;
        return;
    }
}