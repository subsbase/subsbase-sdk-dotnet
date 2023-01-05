using Microsoft.AspNetCore.Mvc;
using SubsBase.SDK;
using SubsBase.SDK.Common.Tests;

namespace SampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly SubsBaseClient _client;
    // inject all services you need here and add them as readonly fields
    public WeatherForecastController(SubsBaseClient client)
    {
        _client = client;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        FieldSelectorBuilderTests test = new FieldSelectorBuilderTests();
        //test.Object_ReturnsFlatSelection();
        //test.SimpleObject_ReturnsFlatSelection();
        //test.ComplexObject_ReturnsFlatSelection();
        
        try
        {
            var hamada = await _client.Query.Customer("test-site_16545217270713391881")
                .Select(c => new
            {
                c.FullName,
                c.EmailAddress,
                PaymentMethod= c.PaymentMethods!
                    .Select( p => new
                    {
                        Id =  p.Id,
                        Type = p.Type
                    })
            }).ExecuteAsync();

            var name = hamada.Value.Customer.FullName;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;
    }
}