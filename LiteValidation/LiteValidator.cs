using LiteValidation.Contracts;

namespace LiteValidation;

public static class LiteValidator
{
    public static ILiteValidatorBuilderForType<T> RuleFor<T>(RuleCheckTypeEnum ruleCheckType, Func<ILiteValidatorRuleOptions<T>, ILiteValidatorRuleOptions<T>> getOptions)
    {
        var options = new LiteValidatorRuleOptions<T>(ruleCheckType);
        getOptions(options);
        return new LiteValidatorBuilder<T>(options);
    }

    public static ILiteValidatorBuilderForType<T> RuleFor<T>(LiteValidatorRuleOptions<T> options)
    {
        return new LiteValidatorBuilder<T>(options);
    }

    public static ILiteValidatorBuilderForValue<T> RuleFor<T>(T value, RuleCheckTypeEnum ruleCheckType, Func<ILiteValidatorRuleOptions<T>, ILiteValidatorRuleOptions<T>> getOptions)
    {
        var options = new LiteValidatorRuleOptions<T>(ruleCheckType);
        getOptions(options);
        return new LiteValidatorBuilder<T>(value, options);
    }

    public static ILiteValidatorBuilderForValue<T> RuleFor<T>(T value, LiteValidatorRuleOptions<T> options)
    {
        return new LiteValidatorBuilder<T>(value, options);
    }
}