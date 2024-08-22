namespace SubsBase.SDK.Balance;

public class IConfigurationConstants
{
    public string BalanceUri;
    public string BalanceMovementUri;
    public string OnHoldAmountUri;
}

public class DevelopmentConstants : IConfigurationConstants
{
    private const string DefaultBaseUrl = "http://api.dev.subsbase.xyz";

    public DevelopmentConstants(string? baseUrl = null)
    {
        if (string.IsNullOrEmpty(baseUrl))
        {
            baseUrl = DefaultBaseUrl;
        }
        
        BalanceUri = $"{baseUrl}/balance/";
        BalanceMovementUri = $"{baseUrl}/balance/balance-movement/";
        OnHoldAmountUri = $"{baseUrl}/balance/on-hold-amount/";
    }
}

public class ProductionConstants : IConfigurationConstants
{
    public ProductionConstants()
    {
        BalanceUri = "http://api.subsbase.io/balance/";
        BalanceMovementUri = "http://api.subsbase.io/balance/balance-movement/";
        OnHoldAmountUri = "http://api.subsbase.io/balance/on-hold-amount/";
    }
}