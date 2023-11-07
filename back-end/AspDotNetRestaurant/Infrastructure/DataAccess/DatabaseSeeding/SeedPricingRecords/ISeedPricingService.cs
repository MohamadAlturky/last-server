using SharedKernal.Utilities.Result;

namespace Infrastructure.DataAccess.DatabaseSeeding.SeedPricingRecords;
public interface ISeedPricingService
{
	Task<Result> SeedPricing();
}
