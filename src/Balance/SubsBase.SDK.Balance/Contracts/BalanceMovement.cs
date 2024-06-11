using System.Text.Json.Serialization;

namespace SubsBase.SDK.Balance.Contracts;

public enum MovementType
{
    Debit,
    Credit
}

public class BalanceMovementNew
{
    [JsonPropertyName("balanceId")] public Guid BalanceId { get; set; }
    [JsonPropertyName("type")] [JsonConverter(typeof(JsonStringEnumConverter))] public MovementType Type { get; set; }
    [JsonPropertyName("amount")] public decimal Amount { get; set; }
    [JsonPropertyName("expirationDate") ] public DateTime? ExpirationDate { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
}