using SubsBase.SDK.Authentication.Service;
using SubsBase.SDK.Common.Contracts;

namespace SubsBase.SDK.Authentication.Client;

public class AuthenticationClient
{
    private readonly AuthenticationService _authService;
    private readonly SubsBaseSdkOptions _options;

    private static string _token;
    private static DateTime _lastTokenRequest;

    public Task<string> ServerToken => GetServerTokenAsync();

    public AuthenticationClient(AuthenticationService auth, SubsBaseSdkOptions options)
    {
        _authService = auth;
        _options = options;
    }

    private async Task<string> GetServerTokenAsync()
    {
        if (_lastTokenRequest > DateTime.Now.AddHours(-4) && _lastTokenRequest <= DateTime.Now)
        {
            return _token;
        }

        _token = await _authService.GenerateTokenAsync(_options.SiteId, _options.ApiSecret);

        if (_token != string.Empty)
        {
            // only update the timestamp if the request was successful
            _lastTokenRequest = DateTime.Now;
        }

        return _token;
    }
}