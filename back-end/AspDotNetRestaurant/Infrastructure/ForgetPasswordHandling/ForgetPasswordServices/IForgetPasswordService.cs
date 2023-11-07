using SharedKernal.Utilities.Result;

namespace Infrastructure.ForgetPasswordHandling.ForgetPasswordServices;
public interface IForgetPasswordService
{
	Task<Result<long>> SendCodeVIAMailAsync(int serialNumber);
	Task<Result> VerifyCode(long entryId, string verificationCode);
}
