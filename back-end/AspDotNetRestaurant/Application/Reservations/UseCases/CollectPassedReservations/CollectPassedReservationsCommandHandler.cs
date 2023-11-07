using Domain.Reservations.Aggregate;
using Domain.Reservations.Repositories;
using Domain.Reservations.Services;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Reservations.UseCases.CollectPassedReservations;
internal class CollectPassedReservationsCommandHandler : ICommandHandler<CollectPassedReservationsCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IReservationRepository _reservationRepository;
	private readonly IReservationsService _reservationsService;

	public CollectPassedReservationsCommandHandler(IUnitOfWork unitOfWork, IReservationRepository reservationRepository, IReservationsService reservationsService)
	{
		_unitOfWork = unitOfWork;
		_reservationRepository = reservationRepository;
		_reservationsService = reservationsService;
	}

	public async Task<Result> Handle(CollectPassedReservationsCommand request, CancellationToken cancellationToken)
	{
		try
		{
			List<Reservation> reservationsToPass =
				_reservationRepository.GetYesterdayReservationsThatPassed();

			_reservationsService.ChangeReservationsToPassed(reservationsToPass);

			_reservationRepository.UpdateAll(reservationsToPass);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success();
		}
		catch (Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
