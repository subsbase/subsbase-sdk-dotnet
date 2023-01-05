using System.Linq.Expressions;
using SubsBase.SDK.Common.Contracts;

namespace SubsBase.SDK.Common.Selectors;

public abstract class PaymentMethodSelector : IFieldSelector<PaymentMethod, PaymentMethodSelector>
{
    public PaymentMethodSelector Select<T>(Expression<Func<PaymentMethod, T>> selector)
    {
        //extract and save selections
        return this;
    }
}