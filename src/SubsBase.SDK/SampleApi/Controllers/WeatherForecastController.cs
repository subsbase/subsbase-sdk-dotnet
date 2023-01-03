using Microsoft.AspNetCore.Mvc;
using SubsBase.SDK.Authentication;
using SubsBase.SDK.Authentication.Client;
using SubsBase.SDK.Authentication.Service;
using SubsBase.SDK.Common;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Common.Tests;
using SubsBase.SDK.Subscription;
using SubsBase.SDK.Subscription.Client;

namespace SampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    // inject all services you need here and add them as readonly fields
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        FieldSelectorBuilderTests test = new FieldSelectorBuilderTests();
        test.Object_ReturnsFlatSelection();
        test.SimpleObject_ReturnsFlatSelection();
        //test.ComplexObject_ReturnsFlatSelection();
        // all of the intermediates return builders and the executeAsync returns the object
        //_client.Query.Customer(CustomerId).select(selector).executeAsync();
        
        
        
        
        
        
        
        
        
        // how will the user utilize the sdk??
        AuthenticationClient client = new AuthenticationClient(new AuthenticationService(new GraphQLClient(new SubsBaseSdkOptions()
        {
            siteId = "test-site",
            apiSecret = "sb_sk_c0650ed23db14d9ba01a765ad56fbcf4"
        })), new SubsBaseSdkOptions()
        {
            siteId = "test-site",
            apiSecret = "sb_sk_c0650ed23db14d9ba01a765ad56fbcf4"
        });
        
        var token = await client.ServerToken;
        
        SubscriptionClient subsClient = new SubscriptionClient(new SubscriptionService(new GraphQLClient(new SubsBaseSdkOptions()
        {
            siteId = "test-site",
            apiSecret = "sb_sk_c0650ed23db14d9ba01a765ad56fbcf4"
        })), client);
        
        var hamada = await subsClient.PauseSubscription;
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}