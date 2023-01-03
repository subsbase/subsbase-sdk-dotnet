using Microsoft.Extensions.DependencyInjection;
using SubsBase.SDK.Common;

namespace SubsBase.SDK.Subscription;

public static class Install
{
    public static IServiceCollection AddSubscriptionSDK(this IServiceCollection services,
        Action<SubsBaseSdkOptions> sdkOptions)
    {
        var options = new SubsBaseSdkOptions();
        sdkOptions(options);

        return services;
    }
}