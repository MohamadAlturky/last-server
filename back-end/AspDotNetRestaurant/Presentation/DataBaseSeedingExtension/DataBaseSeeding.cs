using Infrastructure.Authentication.PasswordHashing;
using Infrastructure.DataAccess.DatabaseSeeding.SeedSuperUsers;
using Infrastructure.DataAccess.DatabaseSeeding.SeedPricingRecords;
using Infrastructure.DataAccess.DBContext;
using SharedKernal.Utilities.Result;

namespace Presentation.DataBaseSeedingExtension;

public static class DataBaseSeeding
{
	public static async Task EnsureDataCompleteness(this WebApplication application)
	{
		using var scope = application.Services.CreateScope();
		RestaurantContext dbContext = scope.ServiceProvider.GetRequiredService<RestaurantContext>();
		IHashHandler hashHandler = new HashHandler();
		ISeedSuperUsersService adminSeeder = new SeedSuperUsersService(dbContext, hashHandler);
		ISeedPricingService pricingSeeder = new SeedPricingService(dbContext);

		Result seedSuperUsersResult =  await adminSeeder.SeedSuperUsers();
		Result seedPriceResult = await pricingSeeder.SeedPricing();

		if(seedPriceResult.IsFailure || seedSuperUsersResult.IsFailure)
		{
			throw new DataMisalignedException(seedSuperUsersResult.Error.Message+"   "+ seedPriceResult.Error.Message);
		}
	}
}
