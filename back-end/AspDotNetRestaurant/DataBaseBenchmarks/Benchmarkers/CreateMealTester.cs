using Application.AssemblyReference;
using Application.UseCases.Meals.Create;
using BenchmarkDotNet.Attributes;
using Domain.Customers.Repositories;
using Domain.Localization;
using Domain.MealInformations.Aggregate;
using Domain.MealInformations.Repositories;
using Domain.Meals.Repositories;
using Domain.Meals.ValueObjects;
using Domain.Reservations.Repositories;
using Domain.Shared.Repositories;
using Infrastructure.CustomersPersistance.Repository;
using Infrastructure.DataAccess.DBContext;
using Infrastructure.DataAccess.MealsInformationPersistance;
using Infrastructure.DataAccess.UnitOfWork;
using Infrastructure.MealsPersistence.Repository;
using Infrastructure.PricingRecordsPersistance.Repository;
using Infrastructure.ReservationsPersistence.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace DataBaseBenchmarks.Benchmarkers;

[MemoryDiagnoser]
public class CreateMealTester
{
	private RestaurantContext _context;
	private IMediator _mediator;

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


		services.AddScoped<IDomainLocalizer, FakeClasses.FakeDomainLocalizer>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IMealInformationRepository, MealInformationRepository>();
		services.AddScoped<IMealEntryRepository, MealEntryRepository>();
		services.AddScoped<IReservationRepository, ReservationRepository>();
		services.AddScoped<ICustomerRepository, CustomerRepository>();
		services.AddScoped<IPricingRepository, PricingRepository>();
		services.AddMediatR(typeof(ApplicationAssemblyReference).Assembly);



		var provider = services.BuildServiceProvider();
		_mediator = provider.GetRequiredService<IMediator>();


		var localizer = provider.GetRequiredService<IDomainLocalizer>();
		LocalizationProvider.SetLocalizer(localizer);

		var optionsBuilder = new DbContextOptionsBuilder<RestaurantContext>();
		optionsBuilder.UseSqlServer(connectionString);
		_context = new RestaurantContext(optionsBuilder.Options);
	}


	[Benchmark]
	public async Task<Result> CallHandlerToCreateMealByMediator()
	{
		var meal = new MealInformation(0)
		{
			ImagePath= "MealsImages\\10b6bbbe-be15-44f3-83a7-eafe266b66e9.png",
			Name="Dummy",
			NumberOfCalories=1000,
			Type=MealType.NormalMeal.ToString(),
			CreatedAt = DateTime.Now,
			Description = "gooood"
		};

		Result response = await _mediator.Send(new CreateMealCommand(meal),CancellationToken.None);
		return response;
	}
	[Benchmark]
	public async Task CreateMealByDbDirectly()
	{
		var meal = new MealInformation(0)
		{
			ImagePath = "MealsImages\\10b6bbbe-be15-44f3-83a7-eafe266b66e9.png",
			Name = "Dummy",
			NumberOfCalories = 1000,
			Type = MealType.UnDefined.ToString(),
			CreatedAt = DateTime.Now,
			Description = "gooood"
		};
		_context.Set<MealInformation>().Add(meal);
		await _context.SaveChangesAsync();
	}

	[Benchmark]
	public async Task<Result> CreateMealByHandler()
	{
		var meal = new MealInformation(0)
		{
			ImagePath = "MealsImages\\10b6bbbe-be15-44f3-83a7-eafe266b66e9.png",
			Name = "Dummy",
			NumberOfCalories = 1000,
			Type = MealType.Dinner.ToString(),
			CreatedAt = DateTime.Now,
			Description = "gooood"
		};
		var handler = new CreateMealCommandHandler(new UnitOfWork(_context), new MealInformationRepository(_context));
		return await handler.Handle(new CreateMealCommand(meal), CancellationToken.None);
	}

}
