using System.Text.Json;
using SubsBase.Common.ApiClientHelper;
using SubsBase.SDK.Balance.Contracts;
using SubsBase.SDK.Balance.Services;

namespace SubsBase.SDK.Balance.Client;

public class BalanceMovementClient
{
    private readonly ApiClient _apiClient;
    private readonly SigningService _signingService;
    private readonly BalanceConfiguration _balanceConfiguration;

    public BalanceMovementClient(ApiClient apiClient, BalanceConfiguration balanceConfiguration)
    {
        _signingService = new SigningService();
        _apiClient = apiClient;
        _balanceConfiguration = balanceConfiguration;
    }
    
    public async Task<Result<BalanceSummary?>> CreateAsync(BalanceMovementNew balanceMovementNew)
    {
        var signaturePayload = new SortedDictionary<string, object>()
        {
            { "amount", balanceMovementNew.Amount },
            { "balanceId", balanceMovementNew.BalanceId.ToString() },
            { "description", balanceMovementNew.Description },
            { "expirationDate", balanceMovementNew.ExpirationDate ?? null},
            { "type", balanceMovementNew.Type.ToString() }
        };
        
        var result = await _apiClient.PostAsync<BalanceMovementNew, BalanceSummary>(
            uri: "balance-movement",
            headers: new Dictionary<string, string>()
            {
                {"publicKey", _balanceConfiguration.PublicKey},
                {"signature",  _signingService.SignPayload(JsonSerializer.Serialize(signaturePayload),_balanceConfiguration.PrivateKey)}
            },
            request: balanceMovementNew);
        
        return result;
    }

    
}