using FluentAssertions;
using SubsBase.SDK.Common.Utils;

namespace SubsBase.SDK.Common.Tests;

public class FieldSelectorBuilderTests
{

    public void SimpleObject_ReturnsFlatSelection()
    {
        var parsed = FieldSelectorBuilder.Parse((Customer c) => new
        {
            c.Name,
            c.Email
        });

        parsed.Should().Be("name,email");
    }
    
    //write more tests
}

internal class Customer
{
    public  string Name { get; set; }
    public string Email { get; set; }
}

internal class PaymentMethodSelector //: ISelector ...
{
    public string Type { get; set; } 
    public string Id { get; set; } 
}
