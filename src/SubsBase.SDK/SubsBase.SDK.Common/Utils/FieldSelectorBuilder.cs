using System.Linq.Expressions;
using System.Reflection;

namespace SubsBase.SDK.Common.Utils;

public class FieldSelectorBuilder
{
    public static string Parse<TIn, TOut>(Expression<Func<TIn, TOut>> expression)
    {
        string selectedFields = String.Empty;
        ParseHelper(expression.Body, ref selectedFields);

        return selectedFields;
    }
    
    private static void ParseHelper(Expression expression, ref string selectedFields)
    {
        if (expression == null)
        {
            return;
        }

        var arguments = (expression as NewExpression).Arguments;

        foreach (var field in arguments)
        {
            if (field.NodeType == ExpressionType.MemberAccess)
            {
                var fieldName = GetFieldName(field);
                AppendSelectedField(fieldName, ref selectedFields);
            }
            else
            {
                string fieldName = GetFieldName((field as MethodCallExpression).Object);
                var subArgs = (field as MethodCallExpression).Arguments.First();
                var unaryExp = (subArgs as UnaryExpression).Operand;
                var lambdaExp = (unaryExp as LambdaExpression).Body;

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
        string fieldName = (expression as MemberExpression).Member.Name;
        return char.ToLower(fieldName[0]) + fieldName.Substring(1);
    }

    private static void AppendSelectedField(string fieldName, ref string selectedFields)
    {
        if (selectedFields == string.Empty)
        {
            selectedFields = fieldName;
            return;
        }
        else if (selectedFields.EndsWith('{'))
        {
            selectedFields += fieldName;
            return;
        }

        selectedFields = string.Join(',', selectedFields, fieldName);
    }
}