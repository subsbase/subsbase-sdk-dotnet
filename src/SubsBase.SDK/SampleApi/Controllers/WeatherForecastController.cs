using Microsoft.AspNetCore.Mvc;
using SubsBase.SDK.Authentication;
using SubsBase.SDK.Authentication.Service;
using SubsBase.SDK.Common;
using SubsBase.SDK.Common.Clients;

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

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
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
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}