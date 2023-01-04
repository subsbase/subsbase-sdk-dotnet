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
            c.FullName
        });
        
        var h = parsed.Should().Be("fullName");
        Console.WriteLine(h);
    }
    
    public void Object_ReturnsFlatSelection()
    {
        var parsed = FieldSelectorBuilder.Parse((Customer c) => new
        {
            c.FullName,
            c.EmailAddress
        });

        parsed.Should().Be("fullName,emailAddress");
    }
    public void ComplexObject_ReturnsFlatSelection()
    {
        var parsed = FieldSelectorBuilder.Parse( (Customer c) => new
        {
            c.FullName,
            c.EmailAddress,
            PaymentMethod = c.PaymentMethods
                .Select( p => new
                {
                    Id =  p.Id,
                    Type = p.Type
                }),
            c.Id
        });

        parsed.Should().Be("fullName,emailAddress,paymentMethods{id,type},id");
    }
}
