using System.Linq.Expressions;

namespace SubsBase.SDK.Common.Utils;

public static class FieldSelectorBuilder
{
    //Takes expression and returns selectors i.e. after the query and inputs args -> {selections}

    public static string Parse<TIn, TOut>(Expression<Func<TIn, TOut>> expression)
    {
        return "";
    }
}