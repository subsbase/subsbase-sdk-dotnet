using Microsoft.Extensions.DependencyInjection;
using SubsBase.SDK.Authentication;
using SubsBase.SDK.Common;
using SubsBase.SDK.Common.Contracts;
using SubsBase.SDK.Subscription;
using SubsBase.SDK.Balance;

namespace SubsBase.SDK;

public static class Install
{
    public static IServiceCollection AddSubsBaseSdk(this IServiceCollection services,
        Action<SubsBaseSdkOptions> sdkOptions)
    {
        services.AddCommonSdk(sdkOptions);
        services.AddAuthenticationSdk();
        services.AddSubscriptionSdk();
        services.AddBalanceSdk();
        services.AddScoped<SubsBaseClient>();

        return services;
    }
}