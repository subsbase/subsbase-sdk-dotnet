namespace subsbase.sdk.AuthorizationApi.Contracts.Requests
{
    internal class AuthRequest
    {
        internal struct Variables{
            public string siteId { get; set; }
            public string apiSecret { get; set; }
        }
        
        public string query { get; set; }
        public string operationName { get; set; }
        public Variables variables { get; set; }
    }
}