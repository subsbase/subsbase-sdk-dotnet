using Microsoft.Extensions.DependencyInjection;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Common.Contracts;

namespace SubsBase.SDK.Common;

public static class Install
{
    public static IServiceCollection AddCommonSdk(this IServiceCollection services,
        Action<SubsBaseSdkOptions> sdkOptions)
    {
        var options = new SubsBaseSdkOptions();
        sdkOptions(options);
        
        services.AddScoped<GraphQlClient>();
        services.AddSingleton<SubsBaseSdkOptions>(options);
        
        return services;
    }
}