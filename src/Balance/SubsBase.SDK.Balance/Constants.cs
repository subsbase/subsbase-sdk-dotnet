namespace SubsBase.SDK.Balance;

public class IConfigurationConstants
{
    public string BalanceUri;
    public string BalanceMovementUri;
    public string OnHoldAmountUri;
}

public class ConfigurationConstants : IConfigurationConstants
{
    private const string DevBaseUrl = "http://api.dev.subsbase.xyz";
    private const string ProdBaseUrl = "http://api.subsbase.io";

    public ConfigurationConstants(Environment environment, string? baseUrl = null)
    {
        baseUrl = !string.IsNullOrEmpty(baseUrl) ? baseUrl.TrimEnd().Trim('/') :
            environment == Environment.Development ? DevBaseUrl : ProdBaseUrl;
        
        BalanceUri = $"{baseUrl}/balance/";
        BalanceMovementUri = $"{baseUrl}/balance/balance-movement/";
        OnHoldAmountUri = $"{baseUrl}/balance/on-hold-amount/";
    }

}
