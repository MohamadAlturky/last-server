using Domain.Anticorruption;
using Domain.Pricing.Aggregate;
using Domain.Shared.Repositories;

namespace Domain.Pricing.SupDomainProxy;
public class PricingRecordsSupDomainProxy : IPricingRecordsSupDomainProxy
{
	private readonly IPricingRepository _pricingRepository;

	public PricingRecordsSupDomainProxy(IPricingRepository pricingRepository)
	{
		_pricingRepository = pricingRepository;
	}
	public PricingRecord? GetPriceByCustomerTypeJoinMealType(string customerType, string mealType)
	{
		return _pricingRepository.GetPriceByCustomerTypeJoinMealType(customerType, mealType);
	}

	public List<PricingRecord> GetPricingRecordsOnMealEntry(long mealEntryId)
	{
		return _pricingRepository.GetByMealEntryId(mealEntryId);
	}
}
