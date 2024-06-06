namespace SubsBase.SDK.Balance.Client;

public class BalanceMovementClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BalanceMovementClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
}