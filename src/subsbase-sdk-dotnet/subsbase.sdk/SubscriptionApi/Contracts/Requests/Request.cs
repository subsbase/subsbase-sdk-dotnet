namespace subsbase.sdk.SubscriptionApi.Contracts.Requests
{
    internal class Request
    {
        internal struct Variables
        {
            public string siteId { get; set; }
            public string customerId { get; set; }
        }
        
        public string query { get; set; }
        public string operationName { get; set; }
        public Variables variables { get; set; }
    }
}