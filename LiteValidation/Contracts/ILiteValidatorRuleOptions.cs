namespace LiteValidation.Contracts;

public interface ILiteValidatorRuleOptions<T>
{
    ILiteValidatorRuleOptions<T> Must(Func<T, bool> predicate);
    ILiteValidatorRuleOptions<T> When(Func<T, bool> predicate);
    ILiteValidatorRuleOptions<T> UseException(Func<Exception> ex);
}
