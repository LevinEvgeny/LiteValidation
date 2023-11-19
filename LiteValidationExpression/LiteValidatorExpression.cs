using LiteValidation;
using LiteValidation.Contracts;
using LiteValidationExpression.Contracts;

namespace LiteValidationExpression;

public static class LiteValidatorExpression
{
    public static ILiteValidatorBuilderForType<T> RuleFor<T>(RuleCheckTypeEnum ruleCheckType, Func<ILiteValidatorExpressionRuleOptions<T>, ILiteValidatorExpressionRuleOptions<T>> getOptions)
    {
        var options = new LiteValidatorExpressionRuleOptions<T>(ruleCheckType);
        getOptions(options);
        return new LiteValidatorBuilder<T>(options);
    }

    public static ILiteValidatorBuilderForType<T> RuleFor<T>(LiteValidatorExpressionRuleOptions<T> options)
    {
        return new LiteValidatorBuilder<T>(options);
    }

    public static ILiteValidatorBuilderForValue<T> RuleFor<T>(T value, RuleCheckTypeEnum ruleCheckType, Func<ILiteValidatorExpressionRuleOptions<T>, ILiteValidatorExpressionRuleOptions<T>> getOptions)
    {
        var options = new LiteValidatorExpressionRuleOptions<T>(ruleCheckType);
        getOptions(options);
        return new LiteValidatorBuilder<T>(value, options);
    }

    public static ILiteValidatorBuilderForValue<T> RuleFor<T>(T value, LiteValidatorExpressionRuleOptions<T> options)
    {
        return new LiteValidatorBuilder<T>(value, options);
    }
}