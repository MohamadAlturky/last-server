using Application.AssemblyReference;
using Application.MealInformations.UseCases.GetPage;
using BenchmarkDotNet.Attributes;
using Domain.Customers.Repositories;
using Domain.Localization;
using Domain.MealInformations.Aggregate;
using Domain.MealInformations.ReadModels;
using Domain.MealInformations.Repositories;
using Domain.Meals.Repositories;
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
public class GetMealsPageTester
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
	public async Task<Result<MealsInformationReadModel>> GetByHandlerCallMediator()
	{

		Result<MealsInformationReadModel> response = await _mediator.Send(new GetMealsInformationPageQuery(60),
			CancellationToken.None);
		return response;
	}
	[Benchmark]
	public async Task<MealsInformationReadModel> GetFromDbDirectly()
	{
		IOrderedQueryable<MealInformation> queryableMeals =
			_context.Set<MealInformation>()
			.OrderByDescending(entry => entry.Id);

		int size = queryableMeals.Count();

		List<MealInformation> meals = queryableMeals
			.Skip(50 * (60- 1))
			.Take(50)
			.ToList();
		MealsInformationReadModel model = new MealsInformationReadModel()
		{
			Count = size,
			MealsInformation = meals
		};
		await _context.SaveChangesAsync();
		return model;
	}
	[Benchmark]
	public async Task<Result<MealsInformationReadModel>> GetByHandler()
	{
		var handler = new GetMealsInformationPageQueryHandler(new UnitOfWork(_context),new MealInformationRepository(_context));
		return await handler.Handle(new GetMealsInformationPageQuery(60), CancellationToken.None);
	}

}
