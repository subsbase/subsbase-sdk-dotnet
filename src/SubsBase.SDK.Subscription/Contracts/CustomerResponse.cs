using Newtonsoft.Json;

namespace SubsBase.SDK.Subscription.Contracts;

public class CustomerResponse
{
    [JsonProperty("customer")] public Customer Customer { get; set; }
}