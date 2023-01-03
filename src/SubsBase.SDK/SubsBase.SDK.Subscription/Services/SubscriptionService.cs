namespace SubsBase.SDK.Subscription;
using SubsBase.SDK.Common.Clients;

public class SubscriptionService
{
    private readonly GraphQLClient _graphQLClient;

    public SubscriptionService(GraphQLClient graphQLClient)
    {
        _graphQLClient = graphQLClient;
    }

    public async Task<string> PauseSubscriptionWithInterval(string siteId, 
        string customerId, 
        string planSubscriptionId, 
        string requesterId, 
        Interval interval)
    {
        await Task.Delay(1);
        return string.Empty;
    }
}

public class Interval
{
    public enum IntervalUnit
    {
        Minute,
        Hour,
        Day,
        Week,
        Month,
        Quarter,
        Year
    }

    public IntervalUnit Unit { get; set; }
    public int Duration { get; set; }
}