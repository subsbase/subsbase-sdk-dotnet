using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace SubsBase.SDK.Common.Clients;

public class GraphQLClient
{
    private readonly SubsBaseSdkOptions _options;
    private GraphQLHttpClient _client;
    
    public GraphQLClient(SubsBaseSdkOptions options)
    {
        _options = options;
    }

    public async Task<dynamic> SendAsync(string endpointUrl, string query, object variables, string operationName, string token)
    {
        _client = new GraphQLHttpClient(endpointUrl, new NewtonsoftJsonSerializer());
        
        setRequestHeaders(token);
        
        var request = new GraphQLRequest()
        {
            Query = query,
            Variables = variables,
            OperationName = operationName
        };
        
        // is it okay to have a dynamic datatype here???????
        var response = await _client.SendQueryAsync<dynamic>(request);
        return response;
    }

    private void setRequestHeaders(string token)
    {
        _client.HttpClient.DefaultRequestHeaders.Add("x-site-id", _options.siteId);
        _client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }
}