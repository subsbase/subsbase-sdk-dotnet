namespace SubsBase.SDK.Subscription;

public static class Constants
{
    public const string CoreEndpoint = "https://api.subsbase.xyz/core/graphql";
    public const string CustomerOperationName = "GetCustomer";
    public const string CustomerQuery = "query GetCustomer($siteId : String!, $customerId: String!){{customer(siteId: $siteId, customerId: $customerId){{{0}}}}}";
}