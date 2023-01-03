using SubsBase.SDK.Authentication.Service;
using SubsBase.SDK.Common;

namespace SubsBase.SDK.Authentication;

public class AuthenticationClient
{
    private readonly AuthenticationService _authService;
    private readonly SubsBaseSdkOptions _options;
    
    private string _token;
    private DateTime _lastTokenRequest;
    
    public Task<string> ServerToken => GetServerToken();
    
    public AuthenticationClient(AuthenticationService auth, SubsBaseSdkOptions options)
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