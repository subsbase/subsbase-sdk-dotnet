using SubsBase.Common.ApiClientHelper;
using SubsBase.SDK.Balance.Contracts;

namespace SubsBase.SDK.Balance.Client;

public class BalanceClient
{
    private readonly ApiClient _apiClient;

    public BalanceClient(IHttpClientFactory httpClientFactory)
    {
        _apiClient = new ApiClient(httpClientFactory, baseAddress: "");
    }
    
    public async Task<Result<BalanceInfo?>> CreateAsync(BalanceInfoNew balanceInfoNew)
    {
        var result = await _apiClient.PostAsync<BalanceInfoNew, BalanceInfo>(
            uri: "",
            request: balanceInfoNew);
        
        return result;
    }
    
    public async Task<Result<BalanceInfo?>> GetAsync(Guid id)
    {
        var result = await _apiClient.GetAsync<BalanceInfo>(uri: $"{id}");
        
        return result;
    }
    
    public async Task<Result<BalanceInfo?>> UpdateAsync(Guid id, BalanceInfoUpdate balanceInfoNew)
    {
        var result = await _apiClient.PutAsync<BalanceInfoUpdate, BalanceInfo>(
            uri: $"{id}",
            request: balanceInfoNew);
        
        return result;
    }
}