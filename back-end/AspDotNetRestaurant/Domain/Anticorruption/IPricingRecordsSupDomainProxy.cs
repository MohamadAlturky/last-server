using Domain.Pricing.Aggregate;

namespace Domain.Anticorruption;
public interface IPricingRecordsSupDomainProxy
{
	PricingRecord? GetPriceByCustomerTypeJoinMealType(string customerType,
		string mealType);

	List<PricingRecord> GetPricingRecordsOnMealEntry(long mealEntryId);
}
