using System.Linq.Expressions;

namespace LiteValidationExpression.Extensions;

public static class PredicateExpressionExtension
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> leftExp, Expression<Func<T, bool>> rightExp)
    {

        ParameterExpression p = leftExp.Parameters[0];

        SubstExpressionVisitor visitor = new SubstExpressionVisitor();
        visitor.subst[rightExp.Parameters[0]] = p;

        Expression body = Expression.AndAlso(leftExp.Body, visitor.Visit(rightExp.Body));
        return Expression.Lambda<Func<T, bool>>(body, p);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> leftExp, Expression<Func<T, bool>> rightExp)
    {
        ParameterExpression p = leftExp.Parameters[0];

        SubstExpressionVisitor visitor = new SubstExpressionVisitor();
        visitor.subst[rightExp.Parameters[0]] = p;

        Expression body = Expression.OrElse(leftExp.Body, visitor.Visit(rightExp.Body));
        return Expression.Lambda<Func<T, bool>>(body, p);
    }
}
