using LiteValidationExpression.Contracts;
using LiteValidationExpression.Extensions;

namespace LiteValidationExpression.Extensions;

public static class LiteValidatorExpressionRuleOptionsExtension
{
    public static ILiteValidatorExpressionRuleOptions<T> Must<T>(this ILiteValidatorExpressionRuleOptions<T> builder, Func<T, bool> predicate)
    {
        return builder.Must(predicate);
    }

    public static ILiteValidatorExpressionRuleOptions<T> NotNull<T>(this ILiteValidatorExpressionRuleOptions<T> builder)
    {
        return builder.Must(x => x != null);
    }

    public static ILiteValidatorExpressionRuleOptions<T> NotNull<T, P>(this ILiteValidatorExpressionRuleOptions<T> builder, Func<T, P?> value) where P : struct
    {
        builder.Must(x => value(x).HasValue);
        return builder;
    }

    public static ILiteValidatorExpressionRuleOptions<T> NotNull<T, P>(this ILiteValidatorExpressionRuleOptions<T> builder, Func<T, P> value) where P : class
    {
        builder.Must(x => value(x) != null);
        return builder;
    }

    public static ILiteValidatorExpressionRuleOptions<IEnumerable<T>> Any<T>(this ILiteValidatorExpressionRuleOptions<IEnumerable<T>> builder)
    {
        builder.Must(x => x != null && x.Any());
        return builder;
    }

    public static ILiteValidatorExpressionRuleOptions<T> Any<T, P>(this ILiteValidatorExpressionRuleOptions<T> builder, Func<T, IEnumerable<P>> value)
    {
        builder.Must(x => value(x) != null && value(x).Any());
        return builder;
    }

    public static ILiteValidatorExpressionRuleOptions<IEnumerable<T>> OnlyOneElement<T>(this ILiteValidatorExpressionRuleOptions<IEnumerable<T>> builder)
    {
        builder.Must(x => x != null && x.Take(2).Count() == 1);
        return builder;
    }

    public static ILiteValidatorExpressionRuleOptions<T> OnlyOneElement<T, P>(this ILiteValidatorExpressionRuleOptions<T> builder, Func<T, IEnumerable<P>> value)
    {
        builder.Must(x => value(x) != null && value(x).Take(2).Count() == 1);
        return builder;
    }

    public static ILiteValidatorExpressionRuleOptions<int> Between<T>(this ILiteValidatorExpressionRuleOptions<int> builder, int min, int max)
    {
        builder.Must(x => x >= min && x <= max);
        return builder;
    }
}
