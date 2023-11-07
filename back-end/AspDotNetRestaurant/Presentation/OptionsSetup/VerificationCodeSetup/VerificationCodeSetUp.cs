using Infrastructure.Settings;
using Infrastructure.Mail.Smtp;
using Microsoft.Extensions.Options;

namespace Presentation.OptionsSetup.VerificationCodeSetup;

public class VerificationCodeSetUp : IConfigureOptions<VerificationCode>
{
	private const string SectionName = "VerificationCode";

	private readonly IConfiguration _configuration;

	public VerificationCodeSetUp(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void Configure(VerificationCode options)
	{
		_configuration.GetSection(SectionName).Bind(options);
	}
}
