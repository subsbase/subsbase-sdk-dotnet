using SubsBase.SDK.Common.Selectors;

namespace SubsBase.SDK.Common.Contracts;

public class Customer
{
    public string? Name { get; set; }
    public string? EmailAddress { get; set; }
    public PaymentMethodSelector? PaymentMethod { get; set; }
}