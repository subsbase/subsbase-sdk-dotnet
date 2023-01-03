using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Reflection;
using SubsBase.SDK.Authentication.Service;
using SubsBase.SDK.Common.Clients;

namespace SubsBase.SDK;

public class SubsBaseClient
{
    private readonly AuthenticationService _authService;
    private readonly GraphQLClient _gqlClient;

    public SubsBaseClient(AuthenticationService authService, GraphQLClient gqlClient)
    {
        _authService = authService;
        _gqlClient = gqlClient;
    }

    public Queries Query { get; set; }
    public Mutations Mutate { get; set; }
}

public class Mutations
{
    public CustomerMutationBuilder Customer(string customerId)
    {
        return new CustomerMutationBuilder();
    }
}

public class CustomerMutationBuilder
{

    //public UpdateInfoFields();
    //public SelectionTypes();
    
    
}

public class Queries
{
    private GraphQLClient _gqlClient;

    public CustomerQueryBuilder Customer(string customerId) => new CustomerQueryBuilder(_gqlClient, customerId);
    public CustomersQueryBuilder Customers() => new CustomersQueryBuilder();
}

public class CustomersQueryBuilder
{
    public CustomersQueryBuilder Select<T>(Expression<Func<Customer, T>> selector)
    {
        return this;
    }

    public CustomersQueryBuilder FilterBy(object filter)
    {
        //save filter for execution step
        return this;
    }
    
    public CustomersQueryBuilder SortBy(object filter)
    {
        //save filter for execution step
        return this;
    }
    
    
}

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

public class Result<T>
{
    public Result(T value)
    {
        value = value;
    }
    public T Value { get; }
    public bool IsSuccess { get; set; }
}

public class Customer
{
    public string? Name { get; set; }
    public string? EmailAddress { get; set; }
    public PaymentMethodSelector PaymentMethod { get; set; }
}

public class PaymentMethod
{
    public string Type { get; set; }
    public string Id { get; }
}

public class PaymentMethodSelector : IFieldSelector<PaymentMethod, PaymentMethodSelector>
{

    public PaymentMethodSelector Select<T>(Expression<Func<PaymentMethod, T>> selector)
    {
        //extract and save selections
        
        //customer(id){name,email,paymentmethod{id,type}}
        return this;
    }
}