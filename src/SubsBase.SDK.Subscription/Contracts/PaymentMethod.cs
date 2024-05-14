using Newtonsoft.Json;

namespace SubsBase.SDK.Subscription.Contracts;

public class PaymentMethod
{
    [JsonProperty("id")] public string? Id { get; set; }

    [JsonProperty("type")] public string? Type { get; set; }
}