using System.Text.Json.Serialization;

namespace SubsBase.SDK.Balance.Contracts;

public class BalanceInfo
{
    public Guid Id { get; set; }
    public string Unit { get; set; } = string.Empty;
    public Dictionary<string, object>? Metadata { get; set; }
    public List<BalanceDetails>? BalanceDetails { get; set; }
}

public class BalanceInfoNew
{
    [JsonPropertyName("allowTotalBalanceToBeNegative")] public bool AllowTotalBalanceToBeNegative { get; set; } = false;
    [JsonPropertyName("metadata")] public Dictionary<string, string>? Metadata { get; set; }
    [JsonPropertyName("unit")] public string Unit { get; set; } = string.Empty;
}


public class BalanceInfoUpdate
{
    [JsonPropertyName("metadata")] public Dictionary<string, object>? Metadata { get; set; }
}

public class BalanceSummary
{
    [JsonPropertyName("balanceId")] public Guid BalanceId { get; set; }
    [JsonPropertyName("totalAmount")] public decimal TotalAmount { get; set; }
    [JsonPropertyName("onHoldAmount")] public decimal OnHoldAmount { get; set; }
    [JsonPropertyName("availableAmount")] public decimal AvailableAmount { get; set; }
}