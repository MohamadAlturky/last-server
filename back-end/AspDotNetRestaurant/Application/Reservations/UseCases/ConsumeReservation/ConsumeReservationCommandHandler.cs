using Domain.Reservations.Aggregate;
using Domain.Reservations.Repositories;
using Domain.Reservations.Services;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Reservations.UseCases.ConsumeReservation;
internal class ConsumeReservationCommandHandler : ICommandHandler<ConsumeReservationCommand,Reservation>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IReservationRepository _reservationRepository;
	private readonly IReservationsService _reservationsService;

	public ConsumeReservationCommandHandler(IUnitOfWork unitOfWork, 
		IReservationRepository reservationRepository, 
		IReservationsService reservationsService)
	{
		_unitOfWork = unitOfWork;
		_reservationRepository = reservationRepository;
		_reservationsService = reservationsService;
	}

	public async Task<Result<Reservation>> Handle(ConsumeReservationCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Reservation? reservationToConsume = 
				_reservationRepository.GetReservationOnMealEntryBySerialNumber(request.mealEntryId,request.serialNumber);
			
			if(reservationToConsume is null)
			{
				throw new Exception("reservationToConsume is null");
			}

			_reservationsService.ConsumeReservation(reservationToConsume);

			_reservationRepository.Update(reservationToConsume);

			await _unitOfWork.SaveChangesAsync();
			
			return Result.Success(reservationToConsume);
		}
		catch(Exception exception)
		{
			return Result.Failure<Reservation>(new SharedKernal.Utilities.Errors.Error("",exception.Message));
		}
	}
}
