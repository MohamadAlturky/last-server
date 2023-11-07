using Domain.Reservations.Aggregate;
using Domain.Reservations.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.Reservations.UseCases.GetBetweenTwoDates;
internal class GetReservationsBetweenTwoDatesQueryHandler : IQueryHandler<GetReservationsBetweenTwoDatesQuery, List<Reservation>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IReservationRepository _reservationRepository;

	public GetReservationsBetweenTwoDatesQueryHandler(IUnitOfWork unitOfWork, IReservationRepository reservationRepository)
	{
		_unitOfWork = unitOfWork;
		_reservationRepository = reservationRepository;
	}

	public async Task<Result<List<Reservation>>> Handle(GetReservationsBetweenTwoDatesQuery request, CancellationToken cancellationToken)
	{
		try
		{
			List<Reservation> reservations = _reservationRepository.GetBetweenTwoDate(request.start, request.end);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success(reservations);

		}
		catch (Exception exception)
		{
			return Result.Failure<List<Reservation>>(new Error("", exception.Message));
		}
	}
}
