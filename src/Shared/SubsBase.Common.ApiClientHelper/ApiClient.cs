using System.Text;
using System.Text.Json;

namespace SubsBase.Common.ApiClientHelper;

public partial class ApiClient
{
    private readonly HttpClient _httpClient;
    
    public ApiClient(IHttpClientFactory httpClientFactory, string baseAddress)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(baseAddress);
    }
    
    public async Task<Result<TResult?>> ResponseAsync<TRequest, TResult>(
        string uri,
        HttpMethod httpMethod,
        TRequest? request = null,
        string? mediaType = null,
        Encoding? encoding = null,
        Dictionary<string, string>? headers = null, 
        JsonSerializerOptions? jsonSerializerOptions = null)
        where TResult : class
        where TRequest : class
    {
        mediaType ??= "application/json";
        encoding ??= Encoding.UTF8;
        
        var content = request != null ? JsonSerializer.Serialize(request, jsonSerializerOptions) : "";

        var httpRequest = new HttpRequestMessage(httpMethod, uri);
        httpRequest.Content = new StringContent(content, encoding, mediaType);
        
        if (headers != null)
        {
            foreach (var header in headers)
            {
                if (_httpClient.DefaultRequestHeaders.Contains(header.Key))
                {
                    _httpClient.DefaultRequestHeaders.Remove(header.Key);
                }
                
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        var httpResponseMessage = await _httpClient.SendAsync(httpRequest);
        
        var stringResponse = await httpResponseMessage.Content.ReadAsStringAsync();

        if (!httpResponseMessage.IsSuccessStatusCode) return Result.Fail<TResult?>(stringResponse);

        var result = JsonSerializer.Deserialize<TResult>(stringResponse, jsonSerializerOptions);
            
        return Result.Ok(result);
    }
    
    public async Task<Result<TResult?>> ResponseAsync<TResult>(
        string uri,
        HttpMethod httpMethod,
        Dictionary<string, string>? headers = null, 
        JsonSerializerOptions? jsonSerializerOptions = null)
        where TResult : class
    {
        var httpRequest = new HttpRequestMessage(httpMethod, uri);
        
        if (headers != null)
        {
            foreach (var header in headers)
            {
                if (_httpClient.DefaultRequestHeaders.Contains(header.Key))
                {
                    _httpClient.DefaultRequestHeaders.Remove(header.Key);
                }
                
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        var httpResponseMessage = await _httpClient.SendAsync(httpRequest);
        
        var stringResponse = await httpResponseMessage.Content.ReadAsStringAsync();

        if (!httpResponseMessage.IsSuccessStatusCode) return Result.Fail<TResult?>(stringResponse);

        var result = JsonSerializer.Deserialize<TResult>(stringResponse, jsonSerializerOptions);
            
        return Result.Ok(result);
    }
}