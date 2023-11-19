using LiteValidation.Contracts;

namespace LiteValidation;

public class LiteValidatorRuleOptions<T> : ILiteValidatorRuleOptions<T>, ILiteValidatorRuleCheck<T>
{
    private readonly List<Func<T, bool>> _conditions;
    private List<Func<T, bool>> _conditionsWhen;
    private Func<Exception> _getException;
    private RuleCheckTypeEnum _ruleCheckType;

    public LiteValidatorRuleOptions(RuleCheckTypeEnum ruleCheckType)
    {
        _ruleCheckType = ruleCheckType;
        _conditions = new List<Func<T, bool>>(4);
    }

    public LiteValidatorRuleOptions(RuleCheckTypeEnum ruleCheckType, Func<ILiteValidatorRuleOptions<T>, ILiteValidatorRuleOptions<T>> getOptions)
    {
        _ruleCheckType = ruleCheckType;
        _conditions = new List<Func<T, bool>>(4);
        getOptions(this);
    }

    ILiteValidatorRuleOptions<T> ILiteValidatorRuleOptions<T>.Must(Func<T, bool> predicate)
    {
        _conditions.Add(predicate);
        return this;
    }

    public ILiteValidatorRuleOptions<T> When(Func<T, bool> predicate)
    {
        if (_conditionsWhen is null)
        {
            _conditionsWhen = new List<Func<T, bool>>();
        }

        _conditionsWhen.Add(predicate);

        return this;
    }

    public ILiteValidatorRuleOptions<T> UseException(Func<Exception> ex)
    {
        _getException = ex;
        return this;
    }

    public void RuleCheck(T value)
    {
        CheckSetup();

        switch (_ruleCheckType)
        {
            case RuleCheckTypeEnum.CheckAll:
                RuleCheckAll(value);
                break;
            case RuleCheckTypeEnum.CheckAny:
                RuleCheckAny(value);
                break;
            default:
                break;
        }
    }

    protected void CheckSetup()
    {
        if (!_conditions.GetEnumerator().MoveNext())
        {
            throw new Exception("Нет условий для проверки");
        }

        if (_getException is null)
        {
            throw new Exception("Не указан exception для ошибки");
        }
    }

    private void RuleCheckAny(T value)
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

        foreach (var condition in _conditions)
        {
            if (condition(value))
            {
                return;
            }
        }

        throw _getException();
    }

    private void RuleCheckAll(T value)
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

        foreach (var condition in _conditions)
        {
            if (!condition(value))
            {
                throw _getException();
            }
        }

    }
}