using LiteValidation;
using LiteValidation.Contracts;
using LiteValidationExpression.Contracts;
using LiteValidationExpression.Extensions;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace LiteValidationExpression;

public class LiteValidatorExpressionRuleOptions<T> : ILiteValidatorExpressionRuleOptions<T>, ILiteValidatorRuleCheck<T>
{
    private static readonly ConcurrentDictionary<LiteValidatorExpressionRuleOptionKey<T>, Func<T, bool>> _conditionsCache;
    private Func<T, bool> _condition;
    private List<Func<T, bool>> _conditionsWhen;
    private Func<Exception> _getException;
    private RuleCheckTypeEnum _ruleCheckType;
    private LiteValidatorExpressionRuleOptionKey<T> _ruleOptionKey;

    static LiteValidatorExpressionRuleOptions()
    {
        _conditionsCache = new ConcurrentDictionary<LiteValidatorExpressionRuleOptionKey<T>, Func<T, bool>>();
    }

    public LiteValidatorExpressionRuleOptions(RuleCheckTypeEnum ruleCheckType)
    {
        Initialize(ruleCheckType);
    }

    public LiteValidatorExpressionRuleOptions(RuleCheckTypeEnum ruleCheckType, Func<ILiteValidatorExpressionRuleOptions<T>, ILiteValidatorExpressionRuleOptions<T>> getOptions)
    {
        Initialize(ruleCheckType);
        getOptions(this);
    }

    private void Initialize(RuleCheckTypeEnum ruleCheckType)
    {
        _ruleCheckType = ruleCheckType;
        _ruleOptionKey = new LiteValidatorExpressionRuleOptionKey<T>(ruleCheckType)
        {
            Conditions = new List<Expression<Func<T, bool>>>(4)
        };
    }

    protected void CheckSetup()
    {
        if (!_ruleOptionKey.Conditions.GetEnumerator().MoveNext())
        {
            throw new Exception("Нет условий для проверки");
        }

        if (_getException is null)
        {
            throw new Exception("Не указан exception для ошибки");
        }
    }

    ILiteValidatorExpressionRuleOptions<T> ILiteValidatorExpressionRuleOptions<T>.Must(Expression<Func<T, bool>> predicate)
    {
        _ruleOptionKey.Conditions.Add(predicate);
        return this;
    }

    public ILiteValidatorExpressionRuleOptions<T> When(Func<T, bool> predicate)
    {
        if (_conditionsWhen is null)
        {
            _conditionsWhen = new List<Func<T, bool>>();
        }

        _conditionsWhen.Add(predicate);

        return this;
    }

    public ILiteValidatorExpressionRuleOptions<T> UseException(Func<Exception> ex)
    {
        _getException = ex;
        return this;
    }

    public void RuleCheck(T value)
    {
        if (_conditionsWhen is not null)
        {
            foreach (var conditionWhen in _conditionsWhen)
            {
                if (!conditionWhen(value))
                {
                    return;
                }
            }
        }

        if (_condition is null)
        {
            _condition = _conditionsCache.GetOrAdd(_ruleOptionKey, x => GetConditionExpression().Compile());
        }

        if (!_condition(value))
        {
            throw _getException();
        }
    }

    private Expression<Func<T, bool>> GetConditionExpression()
    {
        Expression<Func<T, bool>> result = null;

        foreach (var item in _ruleOptionKey.Conditions)
        {
            if (result is null)
            {
                result = item;
            }
            else
            {
                switch (_ruleCheckType)
                {
                    case RuleCheckTypeEnum.CheckAll:
                        result = result.And(item);
                        break;
                    case RuleCheckTypeEnum.CheckAny:
                        result = result.Or(item);
                        break;
                    default:
                        break;
                }
            }
        }

        return result;
    }
}