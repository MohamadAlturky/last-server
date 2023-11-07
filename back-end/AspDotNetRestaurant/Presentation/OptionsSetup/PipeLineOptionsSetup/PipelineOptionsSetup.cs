using Application.Settings;
using Microsoft.Extensions.Options;

namespace Presentation.OptionsSetup.PipLineOptionsSetup;

public class PipelineOptionsSetup : IConfigureOptions<PipeLineSettings>
{
	private const string SectionName = "PipeLineSettings";

	private readonly IConfiguration _configuration;

	public PipelineOptionsSetup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void Configure(PipeLineSettings options)
	{
		_configuration.GetSection(SectionName).Bind(options);
	}
}