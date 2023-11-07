using Domain.Customers.Aggregate;
using Domain.Customers.ValueObjects;
using Domain.MealEntries.Aggregate;
using Domain.Pricing.Aggregate;
using Domain.Shared.Repositories;
using Infrastructure.DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.PricingRecordsPersistance.Repository;
public class PricingRepository : IPricingRepository
{
	private readonly RestaurantContext _context;

	public PricingRepository(RestaurantContext context)
	{
		_context = context;
	}

	public void Add(PricingRecord Entity)
	{
		Entity.Id = 0;
		_context.Set<PricingRecord>().Add(Entity);
	}

	public void Delete(PricingRecord Entity)
	{
		_context.Set<PricingRecord>().Remove(Entity);
	}

	public IEnumerable<PricingRecord> GetAll()
	{
		return _context.Set<PricingRecord>()
			.AsNoTracking()
			.ToList();
	}

	public PricingRecord? GetPriceByCustomerTypeJoinMealType(string customerType, string mealEntryId)
	{
		return _context.Set<PricingRecord>()
			.Where(record => record.CustomerTypeValue == customerType)
			.Where(record => record.MealTypeValue == mealEntryId)
			.AsEnumerable()
			.FirstOrDefault();
	}

	public PricingRecord? GetById(long id)
	{
		return _context.Set<PricingRecord>().Find(id);
	}

	public void Update(PricingRecord Entity)
	{
		_context.Set<PricingRecord>().Update(Entity);
	}

	public List<PricingRecord> GetByMealEntryId(long mealEntryId)
	{
		MealEntry? mealEntry = _context.Set<MealEntry>()
			.Include(meal => meal.MealInformation)
			.FirstOrDefault(meal => meal.Id == mealEntryId);

		if (mealEntry is null)
		{
			throw new Exception("if(mealEntry is null)");
		}
		return _context.Set<PricingRecord>()
			.Where(record => record.MealTypeValue == mealEntry.MealInformation.Type)
			.ToList();
	}

	public List<PricingRecord> GetAllForCustomer(long customerId)
	{
		Customer? customer = _context.Set<Customer>().FirstOrDefault(customer=>customer.Id == customerId);
		
		if(customer is null)
		{
			throw new Exception("if(customer is null)");
		}
		return _context.Set<PricingRecord>().Where(record => record.CustomerTypeValue == customer.Category).ToList();
	}
}
