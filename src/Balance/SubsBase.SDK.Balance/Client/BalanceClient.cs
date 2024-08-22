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

        return await _apiClient.PostAsync<BalanceInfoNew, BalanceSummary >(
            uri: "",
            request: balanceInfoNew,
            headers: new Dictionary<string, string>()
            {
                { "publicKey", _balanceConfiguration.PublicKey },
                { "signature", _signingService.SignPayload(JsonSerializer.Serialize(signaturePayload), _balanceConfiguration.PrivateKey)}
            });
    }

    public async Task<Result<List<BalanceSummary>>> GetAllAsync(IEnumerable<string> ids)
    {
        var signaturePayload = new Dictionary<string, string>()
        {
            { "ids",string.Join(',',ids)}
        };

        return await _apiClient.GetAsync<List<BalanceSummary>>(
            uri: $"?ids={string.Join(',', ids)}",
            headers: new Dictionary<string, string>()
            {
                { "publicKey", _balanceConfiguration.PublicKey },
                {
                    "signature", _signingService.SignPayload(JsonSerializer.Serialize(signaturePayload),
                        _balanceConfiguration.PrivateKey)
                },
            });
    }
    
    public async Task<Result<BalanceDetails?>> GetAsync(Guid id)
    {
        var signaturePayload = new Dictionary<string, string>()
        {
            { "id", id.ToString()}
        };

        return await _apiClient.GetAsync<BalanceDetails>(
            uri: @$"{id}",
            headers: new Dictionary<string, string>()
            {
                { "publicKey", _balanceConfiguration.PublicKey },
                {
                    "signature", _signingService.SignPayload(JsonSerializer.Serialize(signaturePayload),
                        _balanceConfiguration.PrivateKey)
                },
            }); 
    }

    public async Task<Result<BalanceSummary?>> UpdateAsync(Guid id, BalanceInfoUpdate balanceInfoToUpdate)
    {
        var signaturePayload = new Dictionary<string, object>()
        {
            { "id", id },
            { "metadata", JsonSerializer.Serialize(balanceInfoToUpdate.Metadata) }
        };

        return await _apiClient.PutAsync<BalanceInfoUpdate, BalanceSummary>(
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
    }
}