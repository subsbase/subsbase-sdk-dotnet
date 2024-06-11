using Microsoft.Extensions.DependencyInjection;
using SubsBase.SDK.Balance.Client;

namespace SubsBase.SDK.Balance;

public static class Install
{

    public static IServiceCollection AddBalanceSdk(this IServiceCollection services)
    {
        services.AddTransient<BalanceClient>();
        services.AddTransient<BalanceMovementClient>();
        services.AddTransient<OnHoldAmountClient>();
        
        return services;
    }
}