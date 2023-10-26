using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;

namespace LiteValidation.Test.Banchmarks;

[MemoryDiagnoser]
[RankColumn]
//[SimpleJob(launchCount: 1, warmupCount: 10, iterationCount: 300)]
public class ValidationBenchmark
{
    static TestObject TestObj = new TestObject();
    static ILiteValidatorBuilderForType<TestObject> liteValidatorTestObjectForType;
    static ILiteValidatorBuilderForValue<TestObject> liteValidatorTestObjectForValue;
    static LiteValidatorRuleOptions<TestObject> liteValidatorRuleOptions;

    public ValidationBenchmark()
    {
        liteValidatorRuleOptions = new LiteValidatorRuleOptions<TestObject>(x => x
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
            .Must(x => x.StructCollection.Count <= 10));

        liteValidatorTestObjectForType = LiteValidator.RuleFor<TestObject>(liteValidatorRuleOptions);
        liteValidatorTestObjectForValue = LiteValidator.RuleFor(TestObj, liteValidatorRuleOptions);
    }

    [Benchmark]
    public void TestIf_AllRulesInOneFunc()
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
        {
            return;
        }

        throw new Exception("123");
    }

    [Benchmark]
    public void TestIf_AllRulesDividedInto2Parts()
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
        )
        {
            if (TestObj.Number1 < 10
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
                && TestObj.StructCollection.Count <= 10
              )
            {
                return;
            }
        }

        throw new Exception("123");
    }

    [Benchmark]
    public void TestLiteValidatorForValue_AllRulesInOneFunc()
    {
        LiteValidator.RuleFor(TestObj, x => x
            .Must( x => x is not null
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
                     && x.SuperNumber1 is not null
                     && x.SuperNumber1 < 10
                     && x.SuperNumber2 is not null
                     && x.SuperNumber2 < 10
                     && x.SuperNumber3 is not null
                     && x.SuperNumber3 < 10
                     && x.NestedModel1 is not null
                     && x.NestedModel2 is not null
                     && x.ModelCollection is not null
                     && x.ModelCollection.Count <= 10
                     && x.StructCollection is not null
                     && x.StructCollection.Count <= 10)
            .UseException(() => new Exception("123")))
            .Check();
    }

    [Benchmark]
    public void TestLiteValidatorForValue_AllRulesInDifFunc()
    {
        LiteValidator.RuleFor(TestObj, x => x
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
    public void TestLiteValidatorForValue_WithOptionsSingleInstance()
    {
        LiteValidator.RuleFor(TestObj, liteValidatorRuleOptions).Check();
    }

    [Benchmark]
    public void TestLiteValidatorTestObjectForValue_SingleInstance()
    {
        liteValidatorTestObjectForValue.Check();
    }

    [Benchmark]
    public void TestLiteValidatorTestObjectForType_SingleInstance()
    {
        liteValidatorTestObjectForType.Check(TestObj);
    }
}
