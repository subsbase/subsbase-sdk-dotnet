namespace SubsBase.SDK.Common;

public class Constants
{
    public const string authQuery = @"query GetApiToken($siteId: String!, $apiSecret: String!){ getApiToken(siteId: $siteId, apiSecret: $apiSecret) { isSuccess, value, message} }";
    public const string authEndpoint = "https://api.subsbase.xyz/auth";
    public const string authOperationName = "GetApiToken";
}