using System.Text.Json;
using Subsbase.Balance.Inputs;
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
    
    public async Task<Result<NewBalanceMovementResponse?>> CreateAsync(BalanceMovementNew balanceMovementNew)
    {
        var signaturePayload = new SortedDictionary<string, object>()
        {
            { "amount", balanceMovementNew.Amount },
            { "balanceId", balanceMovementNew.BalanceId.ToString() },
            { "description", balanceMovementNew.Description },
            { "expirationDate", balanceMovementNew.ExpirationDate ?? null},
            { "type", balanceMovementNew.Type.ToString() }
        };
        
        var result = await _apiClient.PostAsync<BalanceMovementNew, NewBalanceMovementResponse>(
            uri: "",
            headers: new Dictionary<string, string>()
            {
                {"publicKey", _balanceConfiguration.PublicKey},
                {"signature",  _signingService.SignPayload(JsonSerializer.Serialize(signaturePayload),_balanceConfiguration.PrivateKey)}
            },
            request: balanceMovementNew);
        
        return result;
    }
    
    public async Task<Result<PaginationResult<BalanceMovement>?>> GetAsync(
        Guid balanceId,
        FilterInput? filter = null,
        SortingInput? sorting = null, 
        PaginationInput? pagination = null)
    {
        var signaturePayload = new SortedDictionary<string, object>()
        {
            { "balanceId", balanceId.ToString() }
        };

        var result = await _apiClient.GetAsync<PaginationResult<BalanceMovement>>(
            uri:
            $"?balanceId={balanceId}&{nameof(filter.SearchTerm)}={filter?.SearchTerm}&{nameof(filter.From)}={filter?.From}&{nameof(filter.To)}={filter?.To}" +
            $"&{nameof(sorting)}={sorting?.SortBy}&{nameof(sorting.SortDirection)}={sorting?.SortDirection}&" +
            $"{nameof(pagination.PageNumber)}={pagination?.PageNumber}&{nameof(pagination.PageSize)}={pagination?.PageSize}",
            headers: new Dictionary<string, string>()
            {
                { "publicKey", _balanceConfiguration.PublicKey },
                {
                    "signature",
                    _signingService.SignPayload(JsonSerializer.Serialize(signaturePayload),
                        _balanceConfiguration.PrivateKey)
                }
            });
        return result;
    }


    
}