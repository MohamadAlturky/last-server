using Microsoft.Extensions.Options;
using Presentation.Settings;

namespace Presentation.OptionsSetup.SettingsSetup;

public class ApiSettingsSetup : IConfigureOptions<ApiSettings>
{
	private const string SectionName = "ApiSettings";

	private readonly IConfiguration _configuration;

	public ApiSettingsSetup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void Configure(ApiSettings options)
	{
		_configuration.GetSection(SectionName).Bind(options);
	}
}
