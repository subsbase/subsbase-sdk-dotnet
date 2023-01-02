using SubsBase.SDK.Common.Services;

namespace SubsBase.SDK.Common;

public class SubsBaseClient
{
    private readonly AuthenticationService _authService;
    private readonly SubsBaseSDKOptions _options;
    
    private string _token;
    private DateTime _lastTokenRequest;
    
    public Task<string> ServerToken => GetServerToken();
    
    public SubsBaseClient(AuthenticationService auth, SubsBaseSDKOptions options)
    {
        _authService = auth;
        _options = options;
    }
    
    private async Task<string> GetServerToken()
    {
        if (_lastTokenRequest > DateTime.Now.AddHours(-4) && _lastTokenRequest <= DateTime.Now)
        {
            return _token;
        }
        
        _token = await _authService.GenerateToken(_options.siteId, _options.apiSecret);
        _lastTokenRequest = DateTime.Now;
        
        return _token;
    }
}