namespace LiteValidation.Contracts;

public interface ILiteValidatorRule<T> : ILiteValidatorRuleOptions<T>, ILiteValidatorRuleCheck<T>
{
}
