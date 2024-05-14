using System.Linq.Expressions;

namespace SubsBase.SDK.Common.Selectors;

public interface IFieldSelector<TModel, out TSelector>
{
    TSelector Select<T>(Expression<Func<TModel, T>> selector);
}