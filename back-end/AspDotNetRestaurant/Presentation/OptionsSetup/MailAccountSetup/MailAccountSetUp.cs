using Infrastructure.Mail.Configuration;
using Microsoft.Extensions.Options;

namespace Presentation.OptionsSetup.MailAccountSetup;

public class MailAccountSetUp : IConfigureOptions<MailAccount>
{
	private const string SectionName = "MailAccount";

	private readonly IConfiguration _configuration;

	public MailAccountSetUp(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void Configure(MailAccount options)
	{
		_configuration.GetSection(SectionName).Bind(options);
	}
}
