
using SubsBase.Common.ApiClientHelper;
using SubsBase.SDK.Balance.Client;

namespace SubsBase.SDK.Balance;

public class BalanceSdk
{
    private readonly BalanceConfiguration _configuration;
    private readonly IConfigurationConstants _configurationConstants;
    
    public BalanceSdk(
        string publicKey,
        string privateKey,
        Environment environment = Environment.Development,
        string? environmentBaseUrl = null 
    )
    {
        _configuration =  new BalanceConfiguration(publicKey, privateKey);
        _configurationConstants = environment == Environment.Development
            ? new DevelopmentConstants(environmentBaseUrl)
            : new ProductionConstants();
    }

    public BalanceClient Balance => new BalanceClient(new ApiClient(new HttpClient(), _configurationConstants.BalanceUri), _configuration);
    public BalanceMovementClient BalanceMovement => new BalanceMovementClient(new ApiClient(new HttpClient(), _configurationConstants.BalanceMovementUri), _configuration);
    public OnHoldAmountClient OnHoldAmount => new OnHoldAmountClient(new ApiClient(new HttpClient(), _configurationConstants.OnHoldAmountUri), _configuration);

}