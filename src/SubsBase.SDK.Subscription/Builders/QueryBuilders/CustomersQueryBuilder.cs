using System.Linq.Expressions;
using SubsBase.SDK.Subscription.Contracts;

namespace SubsBase.SDK.Subscription.Builders.QueryBuilders;

public class CustomersQueryBuilder
{
    public CustomersQueryBuilder Select<T>(Expression<Func<Customer, T>> selector)
    {
        return this;
    }

    public CustomersQueryBuilder FilterBy(object filter)
    {
        //save filter for execution step
        return this;
    }

    public CustomersQueryBuilder SortBy(object filter)
    {
        //save filter for execution step
        return this;
    }
}