using System.Linq.Expressions;

namespace LiteValidationExpression.Contracts;

public interface ILiteValidatorExpressionRuleOptions<T>
{
    ILiteValidatorExpressionRuleOptions<T> Must(Expression<Func<T, bool>> predicate);
    ILiteValidatorExpressionRuleOptions<T> When(Func<T, bool> predicate);
    ILiteValidatorExpressionRuleOptions<T> UseException(Func<Exception> ex);
}
