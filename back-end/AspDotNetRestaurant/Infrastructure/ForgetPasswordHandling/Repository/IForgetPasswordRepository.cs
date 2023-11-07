using Infrastructure.ForgetPasswordHandling.Models;

namespace Infrastructure.ForgetPasswordHandling.Repository;
public interface IForgetPasswordRepository
{
	void SaveInforamtion(ForgetPasswordEntry forgetPasswordEntry);
	Task<ForgetPasswordEntry?> GetForgetPasswordEntryAsync(long id);
}
