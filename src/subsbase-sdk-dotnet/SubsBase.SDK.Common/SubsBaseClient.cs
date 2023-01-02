namespace SubsBase.SDK.Common;

public class SubsBaseClient
{
    private readonly AuthenticationService _auth;
    private readonly SubsBaseSDKOptions _config;
    private string _token;
    private DateTime _lastTokenRequest;
    
    public string ServerToken => GetServerToken();

   

    public SubsBaseClient(AuthenticationService auth, SubsBaseSDKOptions config)
    {
        _auth = auth;
        _config = config;
    }
    
    private string GetServerToken()
    {
        //Check latest if within 4 hrs 
        return _token;
        
        //else
        //use auth service to get a new one
    }
    
}

public class AuthenticationService
{
}