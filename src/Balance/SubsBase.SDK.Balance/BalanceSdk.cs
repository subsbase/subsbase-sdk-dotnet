
using SubsBase.Common.ApiClientHelper;
using SubsBase.SDK.Balance.Client;

namespace SubsBase.SDK.Balance;

public class BalanceSdk
{
    private readonly BalanceConfiguration _configuration;

    public BalanceSdk(
        string publicKey,
        string privateKey)
    {
        _configuration =  new BalanceConfiguration(publicKey, privateKey);
    }

    public BalanceClient Balance => new BalanceClient(new ApiClient(new HttpClient(), Constants.BalanceUri), _configuration);
    public BalanceMovementClient BalanceMovement => new BalanceMovementClient(new ApiClient(new HttpClient(), Constants.BalanceMovementUri), _configuration);
    public OnHoldAmountClient OnHoldAmount => new OnHoldAmountClient(new ApiClient(new HttpClient(), Constants.OnHoldAmountUri), _configuration);

}