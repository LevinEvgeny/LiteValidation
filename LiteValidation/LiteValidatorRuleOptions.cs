namespace LiteValidation;

public class LiteValidatorRuleOptions<T> : ILiteValidatorRuleOptions<T>
{
    private readonly List<Func<T, bool>> _conditions;
    private List<Func<T, bool>> _conditionsWhen;
    private Func<Exception> _getException;
    private Action<T> _ruleCheck;
    public Action<T> GetRuleCheck => _ruleCheck;

    public LiteValidatorRuleOptions()
    {
        _conditions = new List<Func<T, bool>>(4);
        UseRuleCheckAll();
    }

    public LiteValidatorRuleOptions(Func<ILiteValidatorRuleOptions<T>, ILiteValidatorRuleOptions<T>> getOptions)
    {
        _conditions = new List<Func<T, bool>>(4);
        _ruleCheck = FuncCheckAll;
        getOptions(this);
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

    public ILiteValidatorRuleOptions<T> UseRuleCheckAny()
    {
        _ruleCheck = FuncCheckAny;
        return this;
    }

    public ILiteValidatorRuleOptions<T> UseRuleCheckAll()
    {
        _ruleCheck = FuncCheckAll;
        return this;
    }

    void FuncCheckAny(T value)
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

    void FuncCheckAll(T value)
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