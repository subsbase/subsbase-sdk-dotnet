using SubsBase.SDK.Subscription;

namespace SubsBase.SDK;

public class SubsBaseClient
{
    public SubsBaseClient(Queries query, Mutations mutate)
    {
        Query = query;
        Mutate = mutate;
    }

    public Queries Query { get; set; }

    public Mutations Mutate { get; set; }
}


