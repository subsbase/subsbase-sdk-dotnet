using System.Text.Json;
using SubsBase.Common.ApiClientHelper;
using SubsBase.SDK.Balance.Contracts;
using SubsBase.SDK.Balance.Services;

namespace SubsBase.SDK.Balance.Client;

public class OnHoldAmountClient
{
    private readonly ApiClient _apiClient;
    private readonly SigningService _signingService;
    private readonly BalanceConfiguration _balanceConfiguration;


    public OnHoldAmountClient(ApiClient apiClient, BalanceConfiguration balanceConfiguration)
    {
        _signingService = new SigningService();
        _apiClient = apiClient;
        _balanceConfiguration = balanceConfiguration;
    }
    
    public async Task<Result<HoldAmountResponse?>> CreateAsync(HoldAmountNew holdAmountNew)
    {
        var signaturePayload = new SortedDictionary<string, object>()
        {
            { "balanceId", holdAmountNew.BalanceId },
            { "amount", holdAmountNew.Amount },
            { "description", holdAmountNew.Description },
            { "expirationDate", holdAmountNew.ExpirationDate }
        };

        var result = await _apiClient.PostAsync<HoldAmountNew, HoldAmountResponse>(
            uri: "",
            request: holdAmountNew,
            headers: new Dictionary<string, string>()
            {
                { "publicKey", _balanceConfiguration.PublicKey },
                { "signature", _signingService.SignPayload(JsonSerializer.Serialize(signaturePayload), _balanceConfiguration.PrivateKey) }
            });
        return result;
    }
    
    public async Task<Result<BalanceSummary?>> DeleteAsync(string onHoldAmountId, bool isCaptured = false)
    {
        var signaturePayload = new SortedDictionary<string,object>()
        {
            {"onHoldAmountId", onHoldAmountId},
            {"isCaptured", isCaptured.ToString()}
        };
        
        var result = await _apiClient.DeleteAsync<BalanceSummary>(
            uri: $"?onHoldAmountId={onHoldAmountId}&isCaptured={isCaptured}",
            headers: new Dictionary<string, string>()
            {
                {"publicKey" , _balanceConfiguration.PublicKey},
                {"signature", _signingService.SignPayload(JsonSerializer.Serialize(signaturePayload),_balanceConfiguration.PrivateKey)}
            });
        return result;
    }

}