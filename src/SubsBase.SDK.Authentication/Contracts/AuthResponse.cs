using Newtonsoft.Json;

namespace SubsBase.SDK.Authentication.Contracts;

public class AuthResponse
{
    public class ApiTokenBody
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
        
        [JsonProperty("value")]
        public string Value { get; set; }
    }
    
    [JsonProperty("getApiToken")]
    public ApiTokenBody GetApiToken { get; set; }
}