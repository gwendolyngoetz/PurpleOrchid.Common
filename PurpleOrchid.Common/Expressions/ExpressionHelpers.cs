using System;
using System.Linq.Expressions;

namespace PurpleOrchid.Common.Expressions
{
    public static class ExpressionHelpers
    {
        public static string GetValue<T>(Expression<Func<T, object>> expression)
        {
            switch (expression.Body)
            {
                // For strings
                case MemberExpression memberExpression:
                    return memberExpression.Member.Name;

                // For other types (int, long, datetime, etc)
                case UnaryExpression unary when unary.Operand is MemberExpression operand:
                    return operand.Member.Name;
            }

            throw new InvalidCastException($"Not a MemberExpression or UnaryExpression. Type was {expression.Body.GetType().Name}.");
        }
    }
}
