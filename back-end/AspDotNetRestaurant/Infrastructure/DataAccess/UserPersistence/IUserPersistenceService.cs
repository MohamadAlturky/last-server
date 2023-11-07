using Infrastructure.Authentication.Models;
using Infrastructure.DataAccess.UserPersistence.Models;
using Infrastructure.ForgetPasswordHandling.Models;
using SharedKernal.Utilities.Result;

namespace Infrastructure.DataAccess.UserPersistence;
public interface IUserPersistenceService
{
	void CreateUser(User user);
	void AddRolesToUser(long userId, List<Role> roles);
	Task ChangePassword(long userId,string oldPassword,string newPassword);
	Result CheckPasswordValidity(int serialNumber, string password);
	User? GetUser(int serialNumber);
	Task<User?> GetUserAsync(int serialNumber);

	void UpdateUserPassword(int serialNumber,string password);
	ForgetPasswordEntry? GetForgetPasswordEntryOnThisDay(long id);
	void UpdateUserInformation(User user);
	Task<CustomersPaginiationResponse> GetUserPaginatedAsync(int pageNumber);
}
