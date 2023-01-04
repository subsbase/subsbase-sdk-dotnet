using System.Linq.Expressions;
using Newtonsoft.Json;
using SubsBase.SDK.Authentication;
using SubsBase.SDK.Authentication.Client;
using SubsBase.SDK.Common;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Common.Contracts;
using SubsBase.SDK.Common.Selectors;
using SubsBase.SDK.Common.Utils;

namespace SubsBase.SDK.Subscription.Builders.QueryBuilders;

public class CustomerQueryBuilder : IFieldSelector<Customer, CustomerQueryBuilder>
{
    private readonly GraphQLClient _graphQLClient;
    private readonly string _customerId;
    private readonly AuthenticationClient _authClient;
    private readonly SubsBaseSdkOptions _options;
    private string selectedFields = string.Empty;


    internal CustomerQueryBuilder(GraphQLClient gqlClient, 
                                string customerId, 
                                AuthenticationClient authClient,
                                SubsBaseSdkOptions options)
    {
        _graphQLClient = gqlClient;
        _customerId = customerId;
        _authClient = authClient;
        _options = options;
    }
    
    public CustomerQueryBuilder Select<T>(Expression<Func<Customer, T>> selector)
    {
        //extract and save selections
        selectedFields = FieldSelectorBuilder.Parse(selector);
        return this;
    }


    public async Task<Result<CustomerResponse>> ExecuteAsync()
    {
        var customerResponse = await GetCustomerInfo();
        return new Result<CustomerResponse>(customerResponse); //with actual fields and actually
    }

    private async Task<CustomerResponse> GetCustomerInfo()
    {
        var token = await _authClient.ServerToken;
        
        // this string needs to be constructed in a better way!!!!!!!!!11
        string query = "query GetCustomer($siteId : String!, $customerId: String!){customer(siteId: $siteId, customerId: $customerId){" + selectedFields + "}}";
        var variables = new
        {
            siteId = _options.siteId,
            customerId= _customerId
        };
        
        var response = await _graphQLClient.SendAsync(Constants.CoreEndpoint, query, variables, Constants.CustomerOperationName, token);
        var deserializedResponse = JsonConvert.DeserializeObject<CustomerResponse>(response.Data.ToString());

        return deserializedResponse;
    }
}

public class CustomerResponse
{
    public class CustomerR : Customer
    {
        [JsonProperty("paymentMethods")]
        public List<PaymentMethod> PaymentMethods { get; set; }
    }
    
    [JsonProperty("customer")]
    public CustomerR Customer { get; set; }
}

