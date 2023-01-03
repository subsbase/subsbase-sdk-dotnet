using System.Linq.Expressions;
using SubsBase.SDK.Common.Contracts;

namespace SubsBase.SDK.Common.Selectors;

public class PaymentMethodSelector : IFieldSelector<PaymentMethod, PaymentMethodSelector>
{
    public PaymentMethodSelector Select<T>(Expression<Func<PaymentMethod, T>> selector)
    {
        //extract and save selections
        
        //customer(id){name,email,paymentmethod{id,type}}
        return this;
    }
}