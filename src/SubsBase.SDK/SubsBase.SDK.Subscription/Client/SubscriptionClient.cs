using SubsBase.SDK.Authentication.Client;

namespace SubsBase.SDK.Subscription.Client;

public class SubscriptionClient
{
    private readonly SubscriptionService _subscriptionService;
    private readonly AuthenticationClient _authenticationClient;
    
    private string _token;
    
    public Task<string> PauseSubscription => PauseSubscriptionHelper();

    public SubscriptionClient(SubscriptionService subscriptionService, AuthenticationClient authenticationClient)
    {
        _subscriptionService = subscriptionService;
        _authenticationClient = authenticationClient;
    }
    
    private async Task<string> PauseSubscriptionHelper()
    {
        _token = await _authenticationClient.ServerToken;
        
        if(_token != String.Empty)
        // to be implemented
        await Task.Delay(1);
        return String.Empty;
    }
}