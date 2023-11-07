using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Quartz;

namespace Infrastructure.BackgroundJobs;
public class ReservationCollectorBackgroundJobSetup : IConfigureOptions<QuartzOptions>
{
	private readonly ScheduleForCollectPassedReservationsSettings _settings;

	public ReservationCollectorBackgroundJobSetup(IOptions<ScheduleForCollectPassedReservationsSettings> options)
	{
		_settings = options.Value;
	}

	public void Configure(QuartzOptions options)
	{
		JobKey jobKey = JobKey.Create(nameof(ReservationCollector));
		options.AddJob<ReservationCollector>(jobBuilder=>jobBuilder.WithIdentity(jobKey))
		.AddTrigger(trigger => trigger
		.ForJob(jobKey)
		.WithCronSchedule(_settings.CronValue));
	}
}
