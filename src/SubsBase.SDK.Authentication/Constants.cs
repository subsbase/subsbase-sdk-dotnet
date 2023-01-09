namespace SubsBase.SDK.Authentication;

public static class Constants
{
    public const string AuthQuery = @"query GetApiToken($siteId: String!, $apiSecret: String!){ getApiToken(siteId: $siteId, apiSecret: $apiSecret) { isSuccess, value, message} }";
    public const string AuthEndpoint = "https://api.subsbase.xyz/auth";
    public const string AuthOperationName = "GetApiToken";
}