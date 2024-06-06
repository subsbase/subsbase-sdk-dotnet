namespace SubsBase.SDK.Balance.Contracts;

public class BalanceHold
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime? Expiry { get; set; }
    
    public Guid BalanceId { get; set; }
    public BalanceInfo? BalanceInfo { get; set; }
}