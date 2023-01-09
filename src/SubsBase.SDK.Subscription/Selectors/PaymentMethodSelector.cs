using System.Linq.Expressions;
using SubsBase.SDK.Common.Selectors;
using SubsBase.SDK.Subscription.Contracts;

namespace SubsBase.SDK.Subscription.Selectors;

public abstract class PaymentMethodSelector : IFieldSelector<PaymentMethod, PaymentMethodSelector>
{
    public PaymentMethodSelector Select<T>(Expression<Func<PaymentMethod, T>> selector)
    {
        //extract and save selections
        return this;
    }
}