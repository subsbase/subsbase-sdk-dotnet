using System.Linq.Expressions;
using System.Reflection;

namespace SubsBase.SDK.Common.Utils;

public static class FieldSelectorBuilder
{
    public static string Parse<TIn, TOut>(Expression<Func<TIn, TOut>> expression)
    {
        var selectedFields = string.Empty;
        ParseHelper(expression.Body, ref selectedFields);

        return selectedFields;
    }
    
    private static void ParseHelper(Expression? expression, ref string selectedFields)
    {
        var arguments = (expression as NewExpression)?.Arguments;

        if (arguments == null)
        {
            return;
        }
        
        foreach (var field in arguments)
        {
            if (field.NodeType == ExpressionType.MemberAccess)
            {
                var fieldName = GetFieldName(field);
                AppendSelectedField(fieldName, ref selectedFields);
            }
            else
            {
                var callExp = field as MethodCallExpression;
                var fieldName = GetFieldName(callExp!.Object!);
                var subArgs = callExp!.Arguments.First();
                var unaryExp = ((subArgs as UnaryExpression)!).Operand;
                var lambdaExp = ((unaryExp as LambdaExpression)!).Body;

                AppendSelectedField(fieldName, ref selectedFields);

                selectedFields += '{';

                ParseHelper(lambdaExp, ref selectedFields);

                selectedFields += '}';
            }
        }

        return;
    }

    private static string GetFieldName(Expression expression)
    {
        var fieldName = (expression as MemberExpression)?.Member.Name;
        return fieldName != null ? char.ToLower(fieldName[0]) + fieldName[1..] : string.Empty;
    }

    private static void AppendSelectedField(string fieldName, ref string selectedFields)
    {
        if (selectedFields == string.Empty)
        {
            selectedFields = fieldName;
            return;
        }
        
        if (selectedFields.EndsWith('{'))
        {
            selectedFields += fieldName;
            return;
        }

        selectedFields = string.Join(',', selectedFields, fieldName);
    }
}