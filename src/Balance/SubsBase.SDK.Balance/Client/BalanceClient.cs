using System.Text.Json;
using SubsBase.Common.ApiClientHelper;
using SubsBase.SDK.Balance.Contracts;
using SubsBase.SDK.Balance.Services;

namespace SubsBase.SDK.Balance.Client;

public class BalanceClient
{
    private readonly ApiClient _apiClient;
    private readonly SigningService _signingService;
    private readonly BalanceConfiguration _balanceConfiguration;

    public BalanceClient(ApiClient apiClient, BalanceConfiguration balanceConfiguration)
    {
        _apiClient = apiClient;
        _balanceConfiguration = balanceConfiguration;
        _signingService = new SigningService();
    }

    public async Task<Result<BalanceSummary?>> CreateAsync(BalanceInfoNew balanceInfoNew)
    {
        var signaturePayload = new SortedDictionary<string, object>()
        {
            { "allowTotalBalanceToBeNegative", balanceInfoNew.AllowTotalBalanceToBeNegative },
            { "metadata", balanceInfoNew.Metadata},
            { "unit", balanceInfoNew.Unit},
        };

        var result = await _apiClient.PostAsync<BalanceInfoNew, BalanceSummary >(
            uri: "",
            request: balanceInfoNew,
            headers: new Dictionary<string, string>()
            {
                { "publicKey", _balanceConfiguration.PublicKey },
                {
                    "signature", _signingService.SignPayload(JsonSerializer.Serialize(signaturePayload),
                        _balanceConfiguration.PrivateKey)
                },
            });

        return result;
    }

    public async Task<Result<BalanceInfo?>> GetAsync(Guid id)
    {
        var signaturePayload = new Dictionary<string, string>()
        {
            { "id", id.ToString()}
        };

        var result = await _apiClient.GetAsync<BalanceInfo>(
            uri: $"{id}",
            headers: new Dictionary<string, string>()
            {
                { "publicKey", _balanceConfiguration.PublicKey },
                {
                    "signature", _signingService.SignPayload(JsonSerializer.Serialize(signaturePayload),
                        _balanceConfiguration.PrivateKey)
                },
            }); 

        return result;
    }

    public async Task<Result<BalanceInfo?>> UpdateAsync(Guid id, BalanceInfoUpdate balanceInfoToUpdate)
    {
        var signaturePayload = new Dictionary<string, object>()
        {
            { "id", id },
            { "metadata", JsonSerializer.Serialize(balanceInfoToUpdate.Metadata) }
        };

        var result = await _apiClient.PutAsync<BalanceInfoUpdate, BalanceInfo>(
            uri: $"{id}",
            request: balanceInfoToUpdate,
            headers: new Dictionary<string, string>()
            {
                { "publicKey", _balanceConfiguration.PublicKey },
                {
                    "signature", _signingService.SignPayload(JsonSerializer.Serialize(signaturePayload),
                        _balanceConfiguration.PrivateKey)
                },
            });

        return result;
    }
}