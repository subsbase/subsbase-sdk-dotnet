using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json.Linq;

namespace SubsBase.SDK.Common.Clients;

public class GraphQLClient
{
    private readonly SubsBaseSdkOptions _options;
    private GraphQLHttpClient _client;
    
    public GraphQLClient(SubsBaseSdkOptions options)
    {
        _options = options;
    }

    public async Task<GraphQLResponse<JObject>> SendAsync(string endpointUrl, string query, object variables, string operationName, string token)
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
        try
        {
            // Use JBOject and then handle deserialization
            var response = await _client.SendQueryAsync<JObject>(request);
            return response;
        }
        catch (Exception e)
        {
            // log exception
            return null;
        }
    }

    private void setRequestHeaders(string token)
    {
        _client.HttpClient.DefaultRequestHeaders.Add("x-site-id", _options.siteId);
        _client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }
}