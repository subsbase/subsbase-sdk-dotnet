using System.Security.Cryptography;
using System.Text;

namespace SubsBase.SDK.Balance.Services;

public class SigningService
{
    public string SignPayload(string payload, string secret)
    {
        using var hasher = new HMACSHA256(Encoding.ASCII.GetBytes(secret));
        var computedHashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(payload));
        var computedHash = string.Join("", computedHashBytes.Select(b => b.ToString("x2")));
        return computedHash;
    }
}