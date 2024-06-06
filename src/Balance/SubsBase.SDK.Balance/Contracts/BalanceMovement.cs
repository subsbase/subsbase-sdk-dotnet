using System.Text.Json.Serialization;

namespace SubsBase.SDK.Balance.Contracts;

public enum MovementType
{
    Debit,
    Credit
}

public class BalanceMovement
{
    public Guid Id { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))] public MovementType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public DateTime? Expiry { get; set; }
    public string Description { get; set; } = string.Empty;
    
    public Guid BalanceId { get; set; }
    public BalanceInfo? BalanceInfo { get; set; }
}


public class BalanceMovementNew
{
    [JsonConverter(typeof(JsonStringEnumConverter))] public MovementType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime? Expiry { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid BalanceId { get; set; }
}