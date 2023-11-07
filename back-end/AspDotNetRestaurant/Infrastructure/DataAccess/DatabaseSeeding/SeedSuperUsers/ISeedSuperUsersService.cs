using SharedKernal.Utilities.Result;

namespace Infrastructure.DataAccess.DatabaseSeeding.SeedSuperUsers;
public interface ISeedSuperUsersService
{
	Task<Result> SeedSuperUsers();
}
