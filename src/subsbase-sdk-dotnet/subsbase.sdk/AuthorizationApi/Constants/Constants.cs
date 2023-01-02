namespace subsbase.sdk.AuthorizationApi.Constants
{
    internal class Constants
    {
        public const string query =
            "query GetApiToken($siteId: String!, $apiSecret: String!) { getApiToken(siteId: $siteId, apiSecret: $apiSecret) { isSuccess, value, message} }";

        public const string operationName = "GetApiToken";

        public const string apiUrl = "https://api.subsbase.xyz/auth";
    }
}