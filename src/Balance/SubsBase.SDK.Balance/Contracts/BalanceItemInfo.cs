using System.Text.Json.Serialization;

namespace SubsBase.SDK.Balance.Contracts;

public class BalanceItemInfo
{
    [JsonPropertyName("balanceId")] public decimal AvailableAmount { get; set; }
    [JsonPropertyName("expirationDate")] public DateTime? ExpirationDate { get; set; }
    [JsonPropertyName("createdAtUtc")] public DateTime? CreatedAtUtc { get; set; }
}