using System.Text.Json.Serialization;

namespace SubsBase.SDK.Balance.Contracts;

public class HoldAmountNew
{
    [JsonPropertyName("balanceId")] public Guid BalanceId { get; set; }
    [JsonPropertyName("amount")] public decimal Amount { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
    [JsonPropertyName("expirationDate")] public DateTime? ExpirationDate { get; set; }
}

public class HoldAmountResponse
{
    [JsonPropertyName("onHoldAmountId")] public string OnHoldAmountId { get; set; }
}