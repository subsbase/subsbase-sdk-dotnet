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
}

public class BalanceMovement
{
    [JsonPropertyName("utcTimestamp")] public DateTime UtcTimestamp { get; set; }
    [JsonPropertyName("createdAtUtc")] public DateTime CreatedAtUtc { get; set; }
    [JsonPropertyName("amount")] public decimal Amount { get; set; }
    [JsonPropertyName("netBalance")] public decimal NetBalance { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
}

public class OnHoldAmount
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;
    [JsonPropertyName("balanceId")] public Guid BalanceId { get; set; }
    [JsonPropertyName("amount")] public decimal Amount { get; set; }
    [JsonPropertyName("onHoldDate")] public DateTime OnHoldDate { get; set; }
    [JsonPropertyName("releaseDate")] public DateTime? ReleaseDate { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
}