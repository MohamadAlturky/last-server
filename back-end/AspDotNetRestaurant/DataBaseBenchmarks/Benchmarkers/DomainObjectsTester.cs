using BenchmarkDotNet.Attributes;
using Domain.MealInformations.Aggregate;
using Domain.MealInformations.ReadModels;
using Infrastructure.DataAccess.DBContext;
using Infrastructure.DataAccess.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Benchmarks.Benchmarkers;
[MemoryDiagnoser]
public class DomainObjectsTester
{
	private RestaurantContext _context;

	private readonly string connectionString =
		"Server=DESKTOP-OO326C9\\SQLEXPRESS;Database=Restaurant; User Id=kh; Password=123;";


	[GlobalSetup]
	public void Setup()
	{
		var services = new ServiceCollection();
		services.AddDbContext<RestaurantContext>((serviceProvider, option) =>
		{
			option.UseSqlServer(connectionString);
		});


		var provider = services.BuildServiceProvider();



		var optionsBuilder = new DbContextOptionsBuilder<RestaurantContext>();
		optionsBuilder.UseSqlServer(connectionString);
		_context = new RestaurantContext(optionsBuilder.Options);
	}

	[Benchmark]
	public async Task<List<MealInformation>> GetFromDbDirectlyUsingDomainObject()
	{
		IOrderedQueryable<MealInformation> queryableMeals =
			_context.Set<MealInformation>()
			.OrderByDescending(entry => entry.Id);

		int size = queryableMeals.Count();

		List<MealInformation> meals = queryableMeals
			.Skip(10 * (1 - 1))
			.Take(10)
			.ToList();
		
		await _context.SaveChangesAsync();
		return meals;
	}
	[Benchmark]
	public async Task<List<DummyMealInformation>> GetFromDbDirectlyUsingDbObject()
	{
		IOrderedQueryable<DummyMealInformation> queryableMeals =
			_context.Set<DummyMealInformation>()
			.OrderByDescending(entry => entry.Id);

		int size = queryableMeals.Count();

		List<DummyMealInformation> meals = queryableMeals
			.Skip(10 * (1 - 1))
			.Take(10)
			.ToList();
		
		await _context.SaveChangesAsync();
		return meals;
	}
}
