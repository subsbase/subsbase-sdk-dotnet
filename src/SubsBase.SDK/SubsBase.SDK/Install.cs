﻿using Microsoft.Extensions.DependencyInjection;
using SubsBase.SDK.Authentication;
using SubsBase.SDK.Authentication.Service;
using SubsBase.SDK.Common;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Subscription;

namespace SubsBase.SDK;

public static class Install
{
    public static IServiceCollection AddSubsBaseSDK(this IServiceCollection services,
        Action<SubsBaseSdkOptions> sdkOptions)
    {
        var options = new SubsBaseSdkOptions();
        sdkOptions(options);

        services.AddScoped<AuthenticationClient>();
        services.AddScoped<GraphQLClient>();
        services.AddSingleton<SubsBaseSdkOptions>();
        services.AddTransient<AuthenticationService>();
        services.AddTransient<SubscriptionService>();
        
        services.AddTransient<SubsBaseClient>();
        
        return services;
    }
}