using Microsoft.Extensions.DependencyInjection;
using SubsBase.SDK.Common.Services;

namespace SubsBase.SDK.Common;

public static class Install
{
    public static IServiceCollection AddSubsBaseSDK(this IServiceCollection services,
        Action<SubsBaseSDKOptions> sdkOptions)
    {
        var options = new SubsBaseSDKOptions();
        sdkOptions(options);

        services.AddScoped<SubsBaseClient>();
        services.AddScoped<GraphQLClient>();
        services.AddSingleton<SubsBaseSDKOptions>();
        services.AddTransient<AuthenticationService>();

        return services;

    }
}