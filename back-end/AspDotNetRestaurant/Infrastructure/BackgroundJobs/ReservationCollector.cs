using Application.Reservations.UseCases.CollectPassedReservations;
using MediatR;
using Quartz;

namespace Infrastructure.BackgroundJobs;
public class ReservationCollector : IJob
{
	private readonly ISender _sender;

	public ReservationCollector(ISender sender)
	{
		_sender = sender;
	}

	public async Task Execute(IJobExecutionContext context)
	{
		await _sender.Send(new CollectPassedReservationsCommand());
	}
}
