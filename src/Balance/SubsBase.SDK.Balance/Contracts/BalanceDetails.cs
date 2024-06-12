namespace SubsBase.SDK.Balance.Contracts;

public class BalanceDetails
{
    public Guid Id { get; set; }
    public decimal Total { get; set; }
    public DateTime? Expiry { get; set; }
    public Guid BalanceId { get; set; }
    public BalanceInfo? BalanceInfo { get; set; }
}