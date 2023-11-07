using Infrastructure.Authentication.JWTOptions;
using Microsoft.Extensions.Options;

namespace Presentation.OptionsSetup.JWTOptionsSetup;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
	private const string SectionName = "JWT";

	private readonly IConfiguration _configuration;

	public JwtOptionsSetup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void Configure(JwtOptions options)
	{
		_configuration.GetSection(SectionName).Bind(options);
	}
}
