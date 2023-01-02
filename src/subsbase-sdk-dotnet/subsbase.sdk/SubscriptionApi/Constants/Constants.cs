namespace subsbase.sdk.SubscriptionApi.Constants
{
    internal class Constants
    {
        public const string query =
            "query GetCustomer($siteId: String!, $customerId: String!) { customer(siteId: $siteId, customerId: $customerId) { fullName, emailAddress, subscriptions } }";
        public const string operationName = "GetCustomer";

        public const string apiUrl = "https://api.subsbase.xyz/core/graphql";
    }
}