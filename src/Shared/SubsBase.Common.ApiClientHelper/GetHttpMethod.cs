using System.Text;
using System.Text.Json;

namespace SubsBase.Common.ApiClientHelper;

public partial class ApiClient
{
    public async Task<Result<TResult?>> GetAsync<TRequest, TResult>(
        string uri,
        TRequest? request = null,
        string? mediaType = null,
        Encoding? encoding = null,
        Dictionary<string, string>? headers = null, 
        JsonSerializerOptions? jsonSerializerOptions = null)
        where TResult : class
        where TRequest : class
    {
        return await ResponseAsync<TRequest, TResult>(
            uri,
            httpMethod: HttpMethod.Get,
            request,
            mediaType,
            encoding,
            headers,
            jsonSerializerOptions);
    }
    
    public async Task<Result<TResult?>> GetAsync<TResult>(
        string uri,
        Dictionary<string, string>? headers = null, 
        JsonSerializerOptions? jsonSerializerOptions = null)
        where TResult : class
    {
        return await ResponseAsync<TResult>(
            uri,
            httpMethod: HttpMethod.Get,
            headers,
            jsonSerializerOptions);
    }
}