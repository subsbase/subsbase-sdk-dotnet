using Microsoft.Extensions.DependencyInjection;
using SubsBase.SDK.Authentication;
using SubsBase.SDK.Common;
using SubsBase.SDK.Common.Contracts;
using SubsBase.SDK.Subscription;

namespace SubsBase.SDK;

public static class Install
{
    public static IServiceCollection AddSubsBaseSdk(this IServiceCollection services,
        Action<SubsBaseSdkOptions> sdkOptions)
    {
        services.AddAuthenticationSdk();
        services.AddCommonSdk(sdkOptions);
        services.AddSubscriptionSdk();
        
        services.AddScoped<SubsBaseClient>();

        return services;
    }
}