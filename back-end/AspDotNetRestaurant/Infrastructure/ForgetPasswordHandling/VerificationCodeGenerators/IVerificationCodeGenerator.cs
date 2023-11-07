namespace Infrastructure.ForgetPasswordHandling.VerificationCodeGenerators;
internal interface IVerificationCodeGenerator
{
	string GenerateVerificationCode();
}
