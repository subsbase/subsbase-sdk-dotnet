using System.Linq.Expressions;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Common.Contracts;
using SubsBase.SDK.Common.Selectors;

namespace SubsBase.SDK.Common.Builders.QueryBuilders;

public class CustomerQueryBuilder : IFieldSelector<Customer, CustomerQueryBuilder>
{
    private readonly GraphQLClient _gqlClient;
    private readonly string _customerId;
    
    
    internal CustomerQueryBuilder(GraphQLClient gqlClient, string customerId)
    {
        _gqlClient = gqlClient;
        _customerId = customerId;
    }
    public CustomerQueryBuilder Select<T>(Expression<Func<Customer, T>> selector)
    {
        //extract and save selections
        
        return this;
    }


    public async Task<Result<Customer>> ExecuteAsync()
    {
        return await Task.FromResult(new Result<Customer>(new Customer())); //with actual fields and actually
    }
}