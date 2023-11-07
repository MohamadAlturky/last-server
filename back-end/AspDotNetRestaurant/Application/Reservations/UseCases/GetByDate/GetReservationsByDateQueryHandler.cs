using Domain.Meals.Repositories;
using Domain.Reservations.Aggregate;
using Domain.Reservations.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.Reservations.UseCases.GetByDate;
internal class GetReservationsByDateQueryHandler : IQueryHandler<GetReservationsByDateQuery, List<Reservation>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IReservationRepository _reservationRepository;

	public GetReservationsByDateQueryHandler(IUnitOfWork unitOfWork, IReservationRepository reservationRepository)
	{
		_unitOfWork = unitOfWork;
		_reservationRepository = reservationRepository;
	}

	public async Task<Result<List<Reservation>>> Handle(GetReservationsByDateQuery request, CancellationToken cancellationToken)
	{
		try
		{
			List<Reservation> reservations = _reservationRepository.GetByDate(request.day);

			await _unitOfWork.SaveChangesAsync();
			
			return Result.Success(reservations);

		}
		catch (Exception exception)
		{
			return Result.Failure<List<Reservation>>(new Error("",exception.Message));
		}
	}
}
