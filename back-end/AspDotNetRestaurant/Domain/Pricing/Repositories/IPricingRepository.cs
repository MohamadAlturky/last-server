using Domain.Pricing.Aggregate;
using SharedKernal.Repositories;
using System.Data;
using System.Linq.Expressions;

namespace Domain.Shared.Repositories;
public interface IPricingRepository :IRepository<PricingRecord>
{
	IEnumerable<PricingRecord> GetAll();
	PricingRecord? GetById(long id);


	void Add(PricingRecord Entity);
	void Update(PricingRecord Entity);
	void Delete(PricingRecord Entity);
	PricingRecord? GetPriceByCustomerTypeJoinMealType(string customerType, string mealType);
	List<PricingRecord> GetByMealEntryId(long mealEntryId);
	List<PricingRecord> GetAllForCustomer(long customerId);
}
