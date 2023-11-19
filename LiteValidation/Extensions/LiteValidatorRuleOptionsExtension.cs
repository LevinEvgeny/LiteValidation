using LiteValidation.Contracts;
using LiteValidation.Extensions;

namespace LiteValidation.Extensions;

public static class LiteValidatorRuleOptionsExtension
{
    public static ILiteValidatorRuleOptions<T> Must<T>(this ILiteValidatorRuleOptions<T> builder, Func<T, bool> predicate)
    {
        return builder.Must(predicate);
    }

    public static ILiteValidatorRuleOptions<T> NotNull<T>(this ILiteValidatorRuleOptions<T> builder)
    {
        return builder.Must(x => x is not null);
    }

    public static ILiteValidatorRuleOptions<T> NotNull<T, P>(this ILiteValidatorRuleOptions<T> builder, Func<T, P?> value) where P : struct
    {
        builder.Must(x => value(x).HasValue);
        return builder;
    }

    public static ILiteValidatorRuleOptions<T> NotNull<T, P>(this ILiteValidatorRuleOptions<T> builder, Func<T, P> value) where P : class
    {
        builder.Must(x => value(x) is not null);
        return builder;
    }

    public static ILiteValidatorRuleOptions<IEnumerable<T>> Any<T>(this ILiteValidatorRuleOptions<IEnumerable<T>> builder)
    {
        builder.Must(x => x?.Any() is true);
        return builder;
    }

    public static ILiteValidatorRuleOptions<T> Any<T, P>(this ILiteValidatorRuleOptions<T> builder, Func<T, IEnumerable<P>> value)
    {
        builder.Must(x => value(x)?.Any() is true);
        return builder;
    }

    public static ILiteValidatorRuleOptions<IEnumerable<T>> OnlyOneElement<T>(this ILiteValidatorRuleOptions<IEnumerable<T>> builder)
    {
        builder.Must(x => x?.Take(2).Count() == 1);
        return builder;
    }

    public static ILiteValidatorRuleOptions<T> OnlyOneElement<T, P>(this ILiteValidatorRuleOptions<T> builder, Func<T, IEnumerable<P>> value)
    {
        builder.Must(x => value(x)?.Take(2).Count() == 1);
        return builder;
    }

    public static ILiteValidatorRuleOptions<int> Between<T>(this ILiteValidatorRuleOptions<int> builder, int min, int max)
    {
        builder.Must(x => x >= min && x <= max);
        return builder;
    }
}
