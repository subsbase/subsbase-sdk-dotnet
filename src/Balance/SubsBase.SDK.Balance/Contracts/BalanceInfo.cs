namespace SubsBase.SDK.Balance.Contracts;

public class BalanceInfo
{
    public Guid Id { get; set; }
    public string Unit { get; set; } = string.Empty;
    public Dictionary<string, object>? Metadata { get; set; }
    
    public List<BalanceMovement>? BalanceMovements { get; set; }
    public List<BalanceDetails>? BalanceDetails { get; set; }
    public List<BalanceHold>? BalanceHolds { get; set; }
}

public class BalanceInfoNew
{
    public string Unit { get; set; } = string.Empty;
    public Dictionary<string, object>? Metadata { get; set; }
}


public class BalanceInfoUpdate
{
    public Dictionary<string, object>? Metadata { get; set; }
}