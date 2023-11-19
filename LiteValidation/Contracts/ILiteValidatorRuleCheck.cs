using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteValidation.Contracts;

public interface ILiteValidatorRuleCheck<T>
{
    void RuleCheck(T value);
}
