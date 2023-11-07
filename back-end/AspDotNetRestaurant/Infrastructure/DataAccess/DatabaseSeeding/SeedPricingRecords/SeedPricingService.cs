using Domain.Customers.ValueObjects;
using Domain.Meals.ValueObjects;
using Domain.Pricing.Aggregate;
using Infrastructure.DataAccess.DBContext;
using SharedKernal.Utilities.Result;

namespace Infrastructure.DataAccess.DatabaseSeeding.SeedPricingRecords;
public class SeedPricingService : ISeedPricingService
{
	private readonly RestaurantContext _context;

	public SeedPricingService(RestaurantContext context)
	{
		_context = context;
	}

	public async Task<Result> SeedPricing()
	{
		if (_context.Set<PricingRecord>().Any())
		{
			return Result.Success();
		}
		_context.Set<PricingRecord>().AddRange(new List<PricingRecord>()
		{
			/////Employee
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.Employee.ToString(),
				MealTypeValue = MealType.NormalMeal.ToString(),
				Price=1000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.Employee.ToString(),
				MealTypeValue = MealType.Plate.ToString(),
				Price=2000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.Employee.ToString(),
				MealTypeValue = MealType.Dinner.ToString(),
				Price=3000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.Employee.ToString(),
				MealTypeValue = MealType.BreakFast.ToString(),
				Price=4000
			},




			////RestautantManager
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.RestautantManager.ToString(),
				MealTypeValue = MealType.NormalMeal.ToString(),
				Price=1000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.RestautantManager.ToString(),
				MealTypeValue = MealType.Plate.ToString(),
				Price=2000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.RestautantManager.ToString(),
				MealTypeValue = MealType.Dinner.ToString(),
				Price=3000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.RestautantManager.ToString(),
				MealTypeValue = MealType.BreakFast.ToString(),
				Price=4000
			},


			//// RichPeople
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.RichPeople.ToString(),
				MealTypeValue = MealType.NormalMeal.ToString(),
				Price=1000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.RichPeople.ToString(),
				MealTypeValue = MealType.Plate.ToString(),
				Price=2000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.RichPeople.ToString(),
				MealTypeValue = MealType.Dinner.ToString(),

				Price=3000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.RichPeople.ToString(),
				MealTypeValue = MealType.BreakFast.ToString(),

				Price=4000
			},


			/////Visitor
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.Visitor.ToString(),
				MealTypeValue = MealType.NormalMeal.ToString(),
				Price=1000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.Visitor.ToString(),
				MealTypeValue = MealType.Plate.ToString(),
				Price=2000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.Visitor.ToString(),
				MealTypeValue = MealType.Dinner.ToString(),
				Price=3000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.Visitor.ToString(),
				MealTypeValue = MealType.BreakFast.ToString(),
				Price=4000
			},
			
			
			
			
			
			////// VeryPoorPeaple

			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.VeryPoorPeaple.ToString(),
				MealTypeValue = MealType.NormalMeal.ToString(),
				Price=1000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.VeryPoorPeaple.ToString(),
				MealTypeValue = MealType.Plate.ToString(),
				Price=2000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.VeryPoorPeaple.ToString(),
				MealTypeValue = MealType.Dinner.ToString(),
				Price=3000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.VeryPoorPeaple.ToString(),
				MealTypeValue = MealType.BreakFast.ToString(),
				Price=4000
			},





			////// PoorPeople
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.PoorPeople.ToString(),
				MealTypeValue = MealType.NormalMeal.ToString(),
				Price=1000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.PoorPeople.ToString(),
				MealTypeValue = MealType.Plate.ToString(),
				Price=2000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.PoorPeople.ToString(),
				MealTypeValue = MealType.Dinner.ToString(),
				Price=3000
			},
			new PricingRecord()
			{
				CustomerTypeValue=CustomerType.PoorPeople.ToString(),
				MealTypeValue = MealType.BreakFast.ToString(),
				Price=4000
			}
		});
		await _context.SaveChangesAsync();
		return Result.Success();
	}
}
