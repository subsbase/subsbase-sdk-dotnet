using System.Linq.Expressions;
using System.Reflection;
using SubsBase.SDK.Common.Selectors;

namespace SubsBase.SDK.Common.Utils;

public class FieldSelectorBuilder
{
    public static string Parse<TIn, TOut>(Expression<Func<TIn, TOut>> expression)
    {
        string selectedFields = String.Empty;

        ParseHelper(expression, ref selectedFields);

        return selectedFields;
    }

    // missing 2 things here to wrap up this
    // 1. find a way to cast the field to the required expression
    // 2. find a way to compare the type to IFieldSelector<TIn, TOut> to be generic; the type is PaymentMethodSelector which should be an IFieldSelector asln
    
    private static void ParseHelper<TIn, TOut>(Expression<Func<TIn, TOut>> expression, ref string selectedFields)
    {
        if (expression == null)
        {
            return;
        }

        var arguments = ((NewExpression)expression.Body).Arguments;

        foreach (var field in arguments)
        {
            var fieldType = field.Type;
            if (fieldType != typeof(PaymentMethodSelector))
            {
                var fieldName = (field as MemberExpression).Member.Name;
                selectedFields = selectedFields == String.Empty ? fieldName
                                : string.Join(',', selectedFields, fieldName);
            }
            else
            {
                //ParseHelper<TIn, TOut>(field, selectedFields);
            }
        }

        return;
    }
}