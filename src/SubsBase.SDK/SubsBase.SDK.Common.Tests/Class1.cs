using FluentAssertions;
using SubsBase.SDK.Common.Contracts;
using SubsBase.SDK.Common.Utils;

namespace SubsBase.SDK.Common.Tests;

public class FieldSelectorBuilderTests
{

    public void SimpleObject_ReturnsFlatSelection()
    {
        var parsed = FieldSelectorBuilder.Parse((Customer c) => new
        {
            c.Name
        });
        
        var h = parsed.Should().Be("name");
        Console.WriteLine(h);
    }
    
    public void Object_ReturnsFlatSelection()
    {
        var parsed = FieldSelectorBuilder.Parse((Customer c) => new
        {
            c.Name,
            c.EmailAddress
        });

        parsed.Should().Be("name,emailAddress");
    }
    public void ComplexObject_ReturnsFlatSelection()
    {
        var parsed = FieldSelectorBuilder.Parse( (Customer c) => new
        {
            c.Name,
            c.EmailAddress,
            PaymentMethod = c.PaymentMethod
                .Select( p => new
                {
                    Id =  p.Id,
                    Type = p.Type
                }),
            c.Id
        });

        parsed.Should().Be("name,emailAddress,paymentMethod{id,type},id");
    }
}
