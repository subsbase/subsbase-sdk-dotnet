namespace SubsBase.SDK.Authentication;

public class AuthResponse
{
    public class GetApiToken
    {
        public bool isSuccess { get; set; }
        public string value { get; set; }
    }
    public GetApiToken getApiToken { get; set; }
}