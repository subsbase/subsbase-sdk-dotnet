using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json.Linq;
using SubsBase.SDK.Common.Contracts;

namespace SubsBase.SDK.Common.Clients;

public class GraphQlClient
{
    private readonly SubsBaseSdkOptions _options;
    private GraphQLHttpClient _client;

    public GraphQlClient(SubsBaseSdkOptions options)
    {
        _options = options;
    }

    public async Task<GraphQLResponse<JObject>?> SendAsync(string endpointUrl,
        string query,
        object variables,
        string operationName,
        string token)
    {
        _client = new GraphQLHttpClient(endpointUrl, new NewtonsoftJsonSerializer());

        SetRequestHeaders(token);

        var request = new GraphQLRequest()
        {
            Query = query,
            Variables = variables,
            OperationName = operationName
        };

        try
        {
            var response = await _client.SendQueryAsync<JObject>(request);
            return response;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    private void SetRequestHeaders(string token)
    {
        _client.HttpClient.DefaultRequestHeaders.Add("x-site-id", _options.SiteId);
        _client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }
}