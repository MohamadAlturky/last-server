// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Benchmarks.Benchmarkers;
using Benchmarks.FakeEntitiesGenerators;
using DataBaseBenchmarks.Benchmarkers;

Console.WriteLine("Hello, Tester!!!!");

//var result1 = BenchmarkRunner.Run<GetMealsPageTester>(

//					ManualConfig
//						.Create(DefaultConfig.Instance)
//						.WithOptions(ConfigOptions.DisableOptimizationsValidator));




//var result2 = BenchmarkRunner.Run<CreateMealTester>(

//					ManualConfig
//						.Create(DefaultConfig.Instance)
//						.WithOptions(ConfigOptions.DisableOptimizationsValidator));





//var result1 = BenchmarkRunner.Run<DummyMealInformationGenerator>(

//					ManualConfig
//						.Create(DefaultConfig.Instance)
//						.WithOptions(ConfigOptions.DisableOptimizationsValidator));






//var result1 = BenchmarkRunner.Run<DomainObjectsTester>(

//					ManualConfig
//						.Create(DefaultConfig.Instance)
//						.WithOptions(ConfigOptions.DisableOptimizationsValidator));


var result1 = BenchmarkRunner.Run<GetWeeklyMealsQueryTester>(

					ManualConfig
						.Create(DefaultConfig.Instance)
						.WithOptions(ConfigOptions.DisableOptimizationsValidator));


