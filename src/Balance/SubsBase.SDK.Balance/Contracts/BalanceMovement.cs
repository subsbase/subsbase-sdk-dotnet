using System.Text.Json.Serialization;

namespace SubsBase.SDK.Balance.Contracts;

public enum MovementType
{
    Debit,
    Credit
}

public class BalanceMovement
{
    [JsonPropertyName("utcTimestamp")] public DateTime UtcTimestamp { get; set; }
    [JsonPropertyName("createdAtUtc")] public DateTime CreatedAtUtc { get; set; }
    [JsonPropertyName("amount")] public decimal Amount { get; set; }
    [JsonPropertyName("netBalance")] public decimal NetBalance { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
}

public class BalanceMovementNew
{
    [JsonPropertyName("balanceId")] public Guid BalanceId { get; set; }
    [JsonPropertyName("type")] [JsonConverter(typeof(JsonStringEnumConverter))] public MovementType Type { get; set; }
    [JsonPropertyName("amount")] public decimal Amount { get; set; }
    [JsonPropertyName("expirationDate") ] public DateTime? ExpirationDate { get; set; }
    [JsonPropertyName("utcTimestamp") ] public DateTime? UtcTimestamp { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
}

public class NewBalanceMovementResponse
{
    [JsonPropertyName("balanceSummary")] public BalanceSummary BalanceSummary { get; set; }
    [JsonPropertyName("balanceMovementId")] public string BalanceMovementId { get; set; } = string.Empty;
}