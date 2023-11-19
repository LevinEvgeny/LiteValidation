using BenchmarkDotNet.Running;
using LiteValidation.Test.Banchmarks;

BenchmarkRunner.Run<ValidationBenchmarkAll>();
//BenchmarkRunner.Run<ValidationBenchmark>();
//BenchmarkRunner.Run<ValidationBenchmarkExpression>();
