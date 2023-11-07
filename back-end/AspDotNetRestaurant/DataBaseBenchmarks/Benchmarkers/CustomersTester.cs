using Application.Customers.UseCases.GetAll;
using BenchmarkDotNet.Attributes;
using Domain.Customers.Aggregate;
using Domain.Customers.Repositories;
using Domain.MealInformations.Repositories;
using Domain.Meals.Repositories;
using Domain.Reservations.Repositories;
using Domain.Shared.Repositories;
using Infrastructure.Authentication.PasswordHashing;
using Infrastructure.CustomersPersistance.Repository;
using Infrastructure.DataAccess.DBContext;
using Infrastructure.DataAccess.MealsInformationPersistance;
using Infrastructure.DataAccess.UnitOfWork;
using Infrastructure.DataAccess.UserPersistence;
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
public class CustomersTester
{
	private RestaurantContext _context;
	private IMediator _mediator;
	
	private readonly string connectionString = 
		"Server=DESKTOP-OO326C9\\SQLEXPRESS;Database=Restaurant; User Id=kh; Password=123;";


	[GlobalSetup]
	public void Setup()
	{
		var services = new ServiceCollection();
		services.AddMediatR(typeof(CustomersTester));
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IMealInformationRepository, MealInformationRepository>();
		services.AddScoped<IMealEntryRepository, MealEntryRepository>();
		services.AddScoped<IReservationRepository, ReservationRepository>();
		services.AddScoped<ICustomerRepository, CustomerRepository>();
		services.AddScoped<IPricingRepository, PricingRepository>();
		services.AddScoped<IUserPersistenceService, UserPersistenceService>();
		services.AddScoped<IHashHandler, HashHandler>();

		var provider = services.BuildServiceProvider();
		_mediator = provider.GetRequiredService<IMediator>();

		var optionsBuilder = new DbContextOptionsBuilder<RestaurantContext>();
		optionsBuilder.UseSqlServer(connectionString);
		_context = new RestaurantContext(optionsBuilder.Options);
	}
	

	[Benchmark]
	public async Task<List<Customer>> GetFromDbDirectly()
	{
		var response = await _context.Set<Customer>().ToListAsync();
		await _context.SaveChangesAsync();
		return response;
	}
	[Benchmark]
	public async Task<List<Customer>> GetByHandler()
	{
		Result<List<Customer>> response = await _mediator.Send(new GetAllCustomersQuery());
		return response.Value;
	}
}
