using Microsoft.Extensions.DependencyInjection;
using SubsBase.SDK.Balance.Client;

namespace SubsBase.SDK.Balance;

public static class Install
{

    public static IServiceCollection AddBalanceSdk(this IServiceCollection services, string publicKey, string privateKey, Environment environment, string? testingEnvironmentUrl = null)
    {
        return services
            .AddSingleton<BalanceSdk>( provider => new BalanceSdk(publicKey, privateKey, environment, testingEnvironmentUrl))
            .AddSingleton<BalanceClient>( provider => provider.GetRequiredService<BalanceSdk>().Balance)
            .AddSingleton<BalanceMovementClient>( provider => provider.GetRequiredService<BalanceSdk>().BalanceMovement)
            .AddSingleton<OnHoldAmountClient>( provider => provider.GetRequiredService<BalanceSdk>().OnHoldAmount);
    }
}