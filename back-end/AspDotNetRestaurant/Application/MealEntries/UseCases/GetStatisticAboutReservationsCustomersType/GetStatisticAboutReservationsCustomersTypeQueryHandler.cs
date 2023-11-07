using Domain.Reservations.ReadModels;
using Domain.Reservations.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.MealEntries.UseCases.GetStatisticAboutReservationsCustomersType;
internal class GetStatisticAboutReservationsCustomersTypeQueryHandler 
	: IQueryHandler<GetStatisticAboutReservationsCustomersTypeQuery, List<ReservationsCustomerTypeReadModel>>
{

	private readonly IReservationRepository _reservationRepository;
	private readonly IUnitOfWork _unitOfWork;

	public GetStatisticAboutReservationsCustomersTypeQueryHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
	{
		_reservationRepository = reservationRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<List<ReservationsCustomerTypeReadModel>>> Handle(GetStatisticAboutReservationsCustomersTypeQuery request,
		CancellationToken cancellationToken)
	{
		try
		{
			List<ReservationsCustomerTypeReadModel> response = _reservationRepository.GetReservationsGroupedByCustomersTypeOnMeal(request.mealEntryId);
			await _unitOfWork.SaveChangesAsync();
			return Result.Success(response);
		}	
		catch(Exception exception)
		{
			return Result.Failure<List<ReservationsCustomerTypeReadModel>>
				(new SharedKernal.Utilities.Errors.Error("",exception.Message));
		}
	}
}
