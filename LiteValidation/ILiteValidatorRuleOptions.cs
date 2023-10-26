namespace LiteValidation;

public interface ILiteValidatorRuleOptions<T>
{
    ILiteValidatorRuleOptions<T> Must(Func<T, bool> predicate);
    ILiteValidatorRuleOptions<T> When(Func<T, bool> predicate);
    ILiteValidatorRuleOptions<T> UseException(Func<Exception> ex);
    ILiteValidatorRuleOptions<T> UseRuleCheckAny();
    ILiteValidatorRuleOptions<T> UseRuleCheckAll();
}
