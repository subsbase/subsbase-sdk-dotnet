using Newtonsoft.Json;
using SubsBase.SDK.Subscription.Selectors;

namespace SubsBase.SDK.Subscription.Contracts;

public class Customer
{
    [JsonProperty("id")] public string? Id { get; set; }

    [JsonProperty("customerId")] public string? CustomerId { get; set; }

    [JsonProperty("fullName")] public string? FullName { get; set; }

    [JsonProperty("emailAddress")] public string? EmailAddress { get; set; }

    [JsonProperty("paymentMethods")] public List<PaymentMethod> PaymentMethodsList { get; set; }

    public PaymentMethodSelector? PaymentMethods { get; set; }
}