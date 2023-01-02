using Microsoft.Extensions.DependencyInjection;

namespace SubsBase.SDK.Common;

public static class Install
{
    public static IServiceCollection AddSubsBaseSDK(this IServiceCollection services,
        Action<SubsBaseSDKOptions> options)
    {
        //TODO:
        var config = new SubsBaseSDKOptions();
        options(config);

        services.AddTransient<AuthenticationService>();
        services.AddScoped<SubsBaseClient>();
        services.AddSingleton<SubsBaseSDKOptions>(config);

        return services;

    }
}