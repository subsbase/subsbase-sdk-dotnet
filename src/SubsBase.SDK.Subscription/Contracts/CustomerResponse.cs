using Newtonsoft.Json;
using SubsBase.SDK.Common.Contracts;

namespace SubsBase.SDK.Subscription.Contracts;

public class CustomerResponse
{
    [JsonProperty("customer")]
    public Customer Customer { get; set; }
}
