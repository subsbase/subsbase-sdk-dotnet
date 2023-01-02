namespace subsbase.sdk.SubscriptionApi.Contracts.Responses
{
    public struct Customer
    {
        public string fullName { get; set; }
        public string emailAddress { get; set; }
    }
    
    internal class Response
    {
        // customer here should include all fields the customer has
        internal struct Data
        {
            public Customer customer { get; set; }
        }
        public Data data { get; set; }
    }
}