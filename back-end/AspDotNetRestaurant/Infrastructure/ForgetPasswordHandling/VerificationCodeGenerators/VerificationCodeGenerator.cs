using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.ForgetPasswordHandling.VerificationCodeGenerators;
internal class VerificationCodeGenerator : IVerificationCodeGenerator
{
	private readonly VerificationCode _verificationCode;
	private readonly Random _random = new Random();
	
	public VerificationCodeGenerator(IOptionsMonitor<VerificationCode> optionsMonitor)
	{
		_verificationCode = optionsMonitor.CurrentValue;
	}

	public string GenerateVerificationCode()
	{
		string code = "";
		for(int index = 0; index < _verificationCode.Size; index++)
		{
			code += _random.Next(1,9).ToString();
		}
		return code;
	}
}
