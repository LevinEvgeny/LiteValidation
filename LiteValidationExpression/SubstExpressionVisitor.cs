using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace LiteValidationExpression;

public class SubstExpressionVisitor : System.Linq.Expressions.ExpressionVisitor
{
    public ConcurrentDictionary<Expression, Expression> subst = new ConcurrentDictionary<Expression, Expression>();

    protected override Expression VisitParameter(ParameterExpression node)
    {
        Expression newValue;
        if (subst.TryGetValue(node, out newValue))
        {
            return newValue;
        }
        return node;
    }
}