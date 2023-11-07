using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Presentation.OptionsSetup.BackGroundJobsSetup;

public class ScheduleForCollectPassedReservationsSettingsSetUp : IConfigureOptions<ScheduleForCollectPassedReservationsSettings>
{
	private const string SectionName = "ScheduleForCollectPassedReservationsSettings";

	private readonly IConfiguration _configuration;

	public ScheduleForCollectPassedReservationsSettingsSetUp(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void Configure(ScheduleForCollectPassedReservationsSettings options)
	{
		_configuration.GetSection(SectionName).Bind(options);
	}
}
