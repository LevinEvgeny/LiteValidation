namespace LiteValidation;

public static class LiteValidator
{
    public static ILiteValidatorBuilderForType<T> RuleFor<T>(Func<ILiteValidatorRuleOptions<T>, ILiteValidatorRuleOptions<T>> getOptions)
    {
        var options = new LiteValidatorRuleOptions<T>();
        getOptions(options);
        return new LiteValidatorBuilder<T>(options);
    }

    public static ILiteValidatorBuilderForType<T> RuleFor<T>(LiteValidatorRuleOptions<T> options)
    {
        return new LiteValidatorBuilder<T>(options);
    }

    public static ILiteValidatorBuilderForValue<T> RuleFor<T>(T value, Func<ILiteValidatorRuleOptions<T>, ILiteValidatorRuleOptions<T>> getOptions)
    {
        var options = new LiteValidatorRuleOptions<T>();
        getOptions(options);
        return new LiteValidatorBuilder<T>(value, options);
    }

    public static ILiteValidatorBuilderForValue<T> RuleFor<T>(T value, LiteValidatorRuleOptions<T> options)
    {
        return new LiteValidatorBuilder<T>(value, options);
    }
}