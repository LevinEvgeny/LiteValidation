namespace LiteValidation;

internal class LiteValidatorBuilder<T> : ILiteValidatorBuilderForType<T>, ILiteValidatorBuilderForValue<T>
{
    private T _value;
    private readonly LiteValidatorRuleOptions<T> _options;

    public LiteValidatorBuilder(LiteValidatorRuleOptions<T> options)
    {
        _options = options;
    }

    public LiteValidatorBuilder(T value, LiteValidatorRuleOptions<T> options)
    {
        _value = value;
        _options = options;
    }

    public void Check()
    {
        _options.GetRuleCheck(_value);
    }

    public void Check(T value)
    {
        _options.GetRuleCheck(value);
    }
}
