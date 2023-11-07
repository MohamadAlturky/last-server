using BenchmarkDotNet.Attributes;
using Infrastructure.DataAccess.DBContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Shared.ReadModels;
using Domain.Meals.Repositories;
using Infrastructure.MealsPersistence.Repository;

namespace Benchmarks.Benchmarkers;

[MemoryDiagnoser]
public class GetWeeklyMealsQueryTester
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

		var optionsBuilder = new DbContextOptionsBuilder<RestaurantContext>();
		optionsBuilder.UseSqlServer(connectionString);
		_context = new RestaurantContext(optionsBuilder.Options);
	}


	[Benchmark]
	public async Task<WeeklyPreparedMeals> GetWeeklyPreparedMeals()
	{
		IMealEntryRepository repo = new MealEntryRepository(_context);
		WeeklyPreparedMeals res = repo.GetWeeklyMealsStartsFrom(DateTime.Today.AddDays(1), 11);
		await _context.SaveChangesAsync();
		return res;
	}

}
