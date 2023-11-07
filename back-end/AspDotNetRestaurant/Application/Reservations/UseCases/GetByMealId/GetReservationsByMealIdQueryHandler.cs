using Domain.Reservations.Aggregate;
using Domain.Reservations.ReadModels;
using Domain.Reservations.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.Reservations.UseCases.GetByMealId;
internal class GetReservationsByMealIdQueryHandler : IQueryHandler<GetReservationsByMealIdQuery, ReservationsReadModel>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IReservationRepository _reservationRepository;

	public GetReservationsByMealIdQueryHandler(IUnitOfWork unitOfWork, 
		IReservationRepository reservationRepository)
	{
		_unitOfWork = unitOfWork;
		_reservationRepository = reservationRepository;
	}

	public async Task<Result<ReservationsReadModel>> Handle(GetReservationsByMealIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			ReservationsReadModel reservations = _reservationRepository.GetReservationsOnMeal(request.mealEntryId);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success(reservations);

		}
		catch (Exception exception)
		{
			return Result.Failure<ReservationsReadModel>(new Error("", exception.Message));
		}
	}
}
