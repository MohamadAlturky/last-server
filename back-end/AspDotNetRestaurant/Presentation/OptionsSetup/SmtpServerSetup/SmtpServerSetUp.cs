using Infrastructure.Mail.Smtp;
using Microsoft.Extensions.Options;
using Presentation.Settings;

namespace Presentation.OptionsSetup.SmtpServerSetup;

public class SmtpServerSetUp : IConfigureOptions<SmtpServer>
{
	private const string SectionName = "SmtpServer";

	private readonly IConfiguration _configuration;

	public SmtpServerSetUp(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void Configure(SmtpServer options)
	{
		_configuration.GetSection(SectionName).Bind(options);
	}
}