using Microsoft.Extensions.DependencyInjection;
using SubsBase.SDK.Subscription.Client;
using SubsBase.SDK.Subscription.Service;

namespace SubsBase.SDK.Subscription;

public static class Install
{
    public static IServiceCollection AddSubscriptionSdk(this IServiceCollection services)
    {
        services.AddScoped<Queries>();
        services.AddScoped<Mutations>();
        services.AddScoped<SubscriptionClient>();
        services.AddTransient<SubscriptionService>();

        return services;
    }
}