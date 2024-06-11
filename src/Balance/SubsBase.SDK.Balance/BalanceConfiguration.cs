namespace SubsBase.SDK.Balance;

public class BalanceConfiguration
{
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
    
    public BalanceConfiguration(string publicKey, string privateKey)
    {
        PublicKey = publicKey ?? throw new ArgumentNullException(nameof(PublicKey));
        PrivateKey = privateKey ?? throw new ArgumentNullException(nameof(PrivateKey));
    }

}