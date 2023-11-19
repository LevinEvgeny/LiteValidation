using LiteValidation.Contracts;

namespace LiteValidation;

public class LiteValidatorBuilder<T> : ILiteValidatorBuilderForType<T>, ILiteValidatorBuilderForValue<T>
{
    private T _value;
    private readonly ILiteValidatorRuleCheck<T> _rules;

    public LiteValidatorBuilder(ILiteValidatorRuleCheck<T> rules)
    {
        _rules = rules;
    }

    public LiteValidatorBuilder(T value, ILiteValidatorRuleCheck<T> rules)
    {
        _value = value;
        _rules = rules;
    }

    public void Check()
    {
        _rules.RuleCheck(_value);
    }

    public void Check(T value)
    {
        _rules.RuleCheck(value);
    }
}
