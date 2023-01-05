using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SubsBase.SDK.Authentication.Client;
using SubsBase.SDK.Authentication.Service;

namespace SubsBase.SDK.Authentication;

public static class Install
{
    public static IServiceCollection AddAuthenticationSdk(this IServiceCollection services)
    {
        services.AddScoped<AuthenticationClient>();
        services.AddTransient<AuthenticationService>();
        
        return services;
    }
}