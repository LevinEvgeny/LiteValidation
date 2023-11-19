using BenchmarkDotNet.Attributes;
using LiteValidation.Contracts;
using LiteValidation.Extensions;
using LiteValidationExpression;
using LiteValidationExpression.Extensions;
using Tests;

namespace LiteValidation.Test.Banchmarks;

[MemoryDiagnoser]
[RankColumn]
[SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 30)]
public class ValidationBenchmarkAll
{
    static TestObject TestObj = new TestObject();
    static ILiteValidatorBuilderForType<TestObject> liteValidatorTestObjectForType;
    static ILiteValidatorBuilderForValue<TestObject> liteValidatorTestObjectForValue;
    static LiteValidatorRuleOptions<TestObject> liteValidatorRuleOptions;

    static ILiteValidatorBuilderForType<TestObject> liteValidatorExpressionTestObjectForType;
    static ILiteValidatorBuilderForValue<TestObject> liteValidatorExpressionTestObjectForValue;
    static LiteValidatorExpressionRuleOptions<TestObject> liteValidatorExpressionRuleOptions;

    public ValidationBenchmarkAll()
    {
        #region LiteValidator
        liteValidatorRuleOptions = new LiteValidatorRuleOptions<TestObject>(RuleCheckTypeEnum.CheckAll, x => x
            .NotNull()
            .NotNull(x => x.Text1)
            .Must(x => x.Text1.Contains('a', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text2)
            .Must(x => x.Text2.Contains('b', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text3)
            .Must(x => x.Text3.Contains('c', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text4)
            .Must(x => x.Text4.Contains('d', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text5)
            .Must(x => x.Text5.Contains('e', StringComparison.InvariantCultureIgnoreCase))
            .Must(x => x.Number1 < 10)
            .Must(x => x.Number2 < 10)
            .Must(x => x.Number3 < 10)
            .Must(x => x.Number4 < 10)
            .Must(x => x.Number5 < 10)
            .NotNull(x => x.SuperNumber1)
            .Must(x => x.SuperNumber1 < 10)
            .NotNull(x => x.SuperNumber2)
            .Must(x => x.SuperNumber2 < 10)
            .NotNull(x => x.SuperNumber3)
            .Must(x => x.SuperNumber3 < 10)
            .NotNull(x => x.NestedModel1)
            .NotNull(x => x.NestedModel2)
            .NotNull(x => x.ModelCollection)
            .Must(x => x.ModelCollection.Count <= 10)
            .NotNull(x => x.StructCollection)
            .Must(x => x.StructCollection.Count <= 10)
            .UseException(() => new Exception("sdf")));
        
        liteValidatorTestObjectForType = LiteValidator.RuleFor<TestObject>(liteValidatorRuleOptions);
        liteValidatorTestObjectForValue = LiteValidator.RuleFor(TestObj, liteValidatorRuleOptions);
        #endregion

        #region LiteValidatorExpression
        liteValidatorExpressionRuleOptions = new LiteValidatorExpressionRuleOptions<TestObject>(RuleCheckTypeEnum.CheckAll, x => x
            .NotNull()
            .NotNull(x => x.Text1)
            .Must(x => x.Text1.Contains('a', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text2)
            .Must(x => x.Text2.Contains('b', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text3)
            .Must(x => x.Text3.Contains('c', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text4)
            .Must(x => x.Text4.Contains('d', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text5)
            .Must(x => x.Text5.Contains('e', StringComparison.InvariantCultureIgnoreCase))
            .Must(x => x.Number1 < 10)
            .Must(x => x.Number2 < 10)
            .Must(x => x.Number3 < 10)
            .Must(x => x.Number4 < 10)
            .Must(x => x.Number5 < 10)
            .NotNull(x => x.SuperNumber1)
            .Must(x => x.SuperNumber1 < 10)
            .NotNull(x => x.SuperNumber2)
            .Must(x => x.SuperNumber2 < 10)
            .NotNull(x => x.SuperNumber3)
            .Must(x => x.SuperNumber3 < 10)
            .NotNull(x => x.NestedModel1)
            .NotNull(x => x.NestedModel2)
            .NotNull(x => x.ModelCollection)
            .Must(x => x.ModelCollection.Count <= 10)
            .NotNull(x => x.StructCollection)
            .Must(x => x.StructCollection.Count <= 10)
            .UseException(() => new Exception("sdf")));

        liteValidatorExpressionTestObjectForType = LiteValidatorExpression.RuleFor<TestObject>(liteValidatorExpressionRuleOptions);
        liteValidatorExpressionTestObjectForValue = LiteValidatorExpression.RuleFor(TestObj, liteValidatorExpressionRuleOptions);
        #endregion
    }

    [Benchmark]
    public void Test_If()
    {
        if (TestObj is not null
            && TestObj.Text1 is not null
            && TestObj.Text1.Contains('a', StringComparison.InvariantCultureIgnoreCase)
            && TestObj.Text2 is not null
            && TestObj.Text2.Contains('b', StringComparison.InvariantCultureIgnoreCase)
            && TestObj.Text3 is not null
            && TestObj.Text3.Contains('c', StringComparison.InvariantCultureIgnoreCase)
            && TestObj.Text4 is not null
            && TestObj.Text4.Contains('d', StringComparison.InvariantCultureIgnoreCase)
            && TestObj.Text5 is not null
            && TestObj.Text5.Contains('e', StringComparison.InvariantCultureIgnoreCase)
        #region
            && TestObj.Number1 < 10
            && TestObj.Number2 < 10
            && TestObj.Number3 < 10
            && TestObj.Number4 < 10
            && TestObj.Number5 < 10
            && TestObj.SuperNumber1 is not null
            && TestObj.SuperNumber1 < 10
            && TestObj.SuperNumber2 is not null
            && TestObj.SuperNumber2 < 10
            && TestObj.SuperNumber3 is not null
            && TestObj.SuperNumber3 < 10
            && TestObj.NestedModel1 is not null
            && TestObj.NestedModel2 is not null
            && TestObj.ModelCollection is not null
            && TestObj.ModelCollection.Count <= 10
            && TestObj.StructCollection is not null
            && TestObj.StructCollection.Count <= 10)
        #endregion
        {
            return;
        }

        throw new Exception("123");
    }

    /// ########
    /// Delegate
    /// ########

    [Benchmark]
    public void Test_Delegate_LiteValidator_ForValue_AllRulesInOneFunc()
    {
        LiteValidator.RuleFor(TestObj, RuleCheckTypeEnum.CheckAll, x => x
            .Must( x => x != null
                     && x.Text1 is not null
                     && x.Text1.Contains('a', StringComparison.InvariantCultureIgnoreCase)
                     && x.Text2 is not null
                     && x.Text2.Contains('b', StringComparison.InvariantCultureIgnoreCase)
                     && x.Text3 is not null
                     && x.Text3.Contains('c', StringComparison.InvariantCultureIgnoreCase)
                     && x.Text4 is not null
                     && x.Text4.Contains('d', StringComparison.InvariantCultureIgnoreCase)
                     && x.Text5 is not null
                     && x.Text5.Contains('e', StringComparison.InvariantCultureIgnoreCase)
                     && x.Number1 < 10
                     && x.Number2 < 10
                     && x.Number3 < 10
                     && x.Number4 < 10
                     && x.Number5 < 10
                     && x.SuperNumber1 != null
                     && x.SuperNumber1 < 10
                     && x.SuperNumber2 != null
                     && x.SuperNumber2 < 10
                     && x.SuperNumber3 != null
                     && x.SuperNumber3 < 10
                     && x.NestedModel1 != null
                     && x.NestedModel2 != null
                     && x.ModelCollection != null
                     && x.ModelCollection.Count <= 10
                     && x.StructCollection != null
                     && x.StructCollection.Count <= 10)
            .UseException(() => new Exception("123")))
            .Check();
    }

    [Benchmark]
    public void Test_Delegate_LiteValidator_ForValue_AllRulesInDifFunc()
    {
        LiteValidator.RuleFor(TestObj, RuleCheckTypeEnum.CheckAll, x => x
            .NotNull()
            .NotNull(x => x.Text1)
            .Must(x => x.Text1.Contains('a', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text2)
            .Must(x => x.Text2.Contains('b', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text3)
            .Must(x => x.Text3.Contains('c', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text4)
            .Must(x => x.Text4.Contains('d', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text5)
            .Must(x => x.Text5.Contains('e', StringComparison.InvariantCultureIgnoreCase))
            .Must(x => x.Number1 < 10)
            .Must(x => x.Number2 < 10)
            .Must(x => x.Number3 < 10)
            .Must(x => x.Number4 < 10)
            .Must(x => x.Number5 < 10)
            .NotNull(x => x.SuperNumber1)
            .Must(x => x.SuperNumber1 < 10)
            .NotNull(x => x.SuperNumber2)
            .Must(x => x.SuperNumber2 < 10)
            .NotNull(x => x.SuperNumber3)
            .Must(x => x.SuperNumber3 < 10)
            .NotNull(x => x.NestedModel1)
            .NotNull(x => x.NestedModel2)
            .NotNull(x => x.ModelCollection)
            .Must(x => x.ModelCollection.Count <= 10)
            .NotNull(x => x.StructCollection)
            .Must(x => x.StructCollection.Count <= 10)
            .UseException(() => new Exception("123")))
            .Check();
    }

    [Benchmark]
    public void Test_Delegate_LiteValidator_ForValue_WithOptionsSingleInstance()
    {
        LiteValidator.RuleFor(TestObj, liteValidatorRuleOptions).Check();
    }

    [Benchmark]
    public void Test_Delegate_LiteValidator_TestObject_ForValue_SingleInstance()
    {
        liteValidatorTestObjectForValue.Check();
    }

    [Benchmark]
    public void Test_Delegate_LiteValidator_TestObject_ForType_SingleInstance()
    {
        liteValidatorTestObjectForType.Check(TestObj);
    }

    /// ##########
    /// Expression
    /// ##########

    [Benchmark]
    public void Test_Expression_LiteValidator_ForValue_AllRulesInOneFunc()
    {
        LiteValidatorExpression.RuleFor(TestObj, RuleCheckTypeEnum.CheckAll, x => x
            .Must(x => x != null
                     && x.Text1 != null
                     && x.Text1.Contains('a', StringComparison.InvariantCultureIgnoreCase)
                     && x.Text2 != null
                     && x.Text2.Contains('b', StringComparison.InvariantCultureIgnoreCase)
                     && x.Text3 != null
                     && x.Text3.Contains('c', StringComparison.InvariantCultureIgnoreCase)
                     && x.Text4 != null
                     && x.Text4.Contains('d', StringComparison.InvariantCultureIgnoreCase)
                     && x.Text5 != null
                     && x.Text5.Contains('e', StringComparison.InvariantCultureIgnoreCase)
                     && x.Number1 < 10
                     && x.Number2 < 10
                     && x.Number3 < 10
                     && x.Number4 < 10
                     && x.Number5 < 10
                     && x.SuperNumber1 != null
                     && x.SuperNumber1 < 10
                     && x.SuperNumber2 != null
                     && x.SuperNumber2 < 10
                     && x.SuperNumber3 != null
                     && x.SuperNumber3 < 10
                     && x.NestedModel1 != null
                     && x.NestedModel2 != null
                     && x.ModelCollection != null
                     && x.ModelCollection.Count <= 10
                     && x.StructCollection != null
                     && x.StructCollection.Count <= 10)
            .UseException(() => new Exception("123")))
            .Check();
    }

    [Benchmark]
    public void Test_Expression_LiteValidator_ForValue_AllRulesInDifFunc()
    {
        LiteValidatorExpression.RuleFor(TestObj, RuleCheckTypeEnum.CheckAll, x => x
            .NotNull()
            .NotNull(x => x.Text1)
            .Must(x => x.Text1.Contains('a', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text2)
            .Must(x => x.Text2.Contains('b', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text3)
            .Must(x => x.Text3.Contains('c', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text4)
            .Must(x => x.Text4.Contains('d', StringComparison.InvariantCultureIgnoreCase))
            .NotNull(x => x.Text5)
            .Must(x => x.Text5.Contains('e', StringComparison.InvariantCultureIgnoreCase))
            .Must(x => x.Number1 < 10)
            .Must(x => x.Number2 < 10)
            .Must(x => x.Number3 < 10)
            .Must(x => x.Number4 < 10)
            .Must(x => x.Number5 < 10)
            .NotNull(x => x.SuperNumber1)
            .Must(x => x.SuperNumber1 < 10)
            .NotNull(x => x.SuperNumber2)
            .Must(x => x.SuperNumber2 < 10)
            .NotNull(x => x.SuperNumber3)
            .Must(x => x.SuperNumber3 < 10)
            .NotNull(x => x.NestedModel1)
            .NotNull(x => x.NestedModel2)
            .NotNull(x => x.ModelCollection)
            .Must(x => x.ModelCollection.Count <= 10)
            .NotNull(x => x.StructCollection)
            .Must(x => x.StructCollection.Count <= 10)
            .UseException(() => new Exception("123")))
            .Check();
    }

    [Benchmark]
    public void Test_Expression_LiteValidator_ForValue_WithOptionsSingleInstance()
    {
        LiteValidatorExpression.RuleFor(TestObj, liteValidatorExpressionRuleOptions).Check();
    }

    [Benchmark]
    public void Test_Expression_LiteValidator_TestObject_ForValue_SingleInstance()
    {
        liteValidatorExpressionTestObjectForValue.Check();
    }

    [Benchmark]
    public void Test_Expression_LiteValidator_TestObject_ForType_SingleInstance()
    {
        liteValidatorExpressionTestObjectForType.Check(TestObj);
    }
}
