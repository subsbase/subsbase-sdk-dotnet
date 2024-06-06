using Microsoft.Extensions.DependencyInjection;
using SubsBase.SDK.Balance.Client;

namespace SubsBase.SDK.Balance;

public static class Install
{
    public static IServiceCollection AddBalanceServices(this IServiceCollection services)
    {
        services.AddTransient<BalanceClient>();
    
        return services;
    }
}