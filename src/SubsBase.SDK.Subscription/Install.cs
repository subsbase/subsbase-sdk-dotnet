using Microsoft.Extensions.DependencyInjection;
using SubsBase.SDK.Subscription.Service;

namespace SubsBase.SDK.Subscription;

public static class Install
{
    public static IServiceCollection AddSubscriptionSdk(this IServiceCollection services)
    {
        services.AddScoped<Queries>();
        services.AddScoped<Mutations>();
        services.AddTransient<SubscriptionService>();
        
        return services;
    }
}