namespace subsbase.sdk.AuthorizationApi.Contracts.Responses
{
    internal class AuthResponse
    {
        internal struct GetApiToken
        {
            public bool success { get; set; }
            public string value { get; set; }
        }
        internal struct Data
        {
            public GetApiToken getApiToken { get; set; }
        }
        public Data data { get; set; }
    }
}