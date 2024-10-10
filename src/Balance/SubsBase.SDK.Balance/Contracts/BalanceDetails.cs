using System.Text.Json.Serialization;

namespace SubsBase.SDK.Balance.Contracts;

public class BalanceDetails
{
    [JsonPropertyName("balanceId")] public Guid BalanceId { get; set; }
    [JsonPropertyName("metadata")] public Dictionary<string, string>? Metadata { get; set; }
    [JsonPropertyName("unit")] public string Unit { get; set; }
    [JsonPropertyName("allowTotalBalanceToBeNegative")] public bool AllowTotalBalanceToBeNegative { get; set; } = false;
    [JsonPropertyName("balanceSummary")] public BalanceSummary BalanceSummary { get; set; } = new();
    [JsonPropertyName("movements")] public List<BalanceMovement>? Movements { get; set; }
    [JsonPropertyName("onHoldAmounts")] public List<OnHoldAmount>? OnHoldAmounts { get; set; }
    [JsonPropertyName("balanceItems")] public List<BalanceItemInfo>? BalanceItems { get; set; }
}
