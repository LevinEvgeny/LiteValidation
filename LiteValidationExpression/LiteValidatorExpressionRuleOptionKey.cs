using LiteValidation;
using System.Linq.Expressions;

namespace LiteValidationExpression;

public class LiteValidatorExpressionRuleOptionKey<T>
{
    public List<Expression<Func<T, bool>>> Conditions { get; init; }
    public RuleCheckTypeEnum RuleCheckType { get; init; }

    public LiteValidatorExpressionRuleOptionKey(RuleCheckTypeEnum ruleCheckType)
    {
        RuleCheckType = ruleCheckType;
    }

    // override object.Equals
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var objForEquals = (LiteValidatorExpressionRuleOptionKey<T>)obj;

        if (objForEquals.Conditions.Count != Conditions.Count)
        {
            return false;
        }

        var objForEqualsEnumerator = objForEquals.Conditions.GetEnumerator();
        foreach (var item in Conditions)
        {
            objForEqualsEnumerator.MoveNext();
            if (item.Equals(objForEqualsEnumerator.Current))
            {
                return false;
            }
        }

        if (objForEquals.RuleCheckType != RuleCheckType)
        {
            return false;
        }

        return true;
    }

    // override object.GetHashCode
    public override int GetHashCode()
    {
        int  result = Conditions.Count;
        result = result << RuleCheckType.GetHashCode();

        return result;
    }
}
