using Domain.Anticorruption;
using Domain.Customers.Aggregate;
using Domain.MealEntries.Aggregate;
using Domain.Reservations.Aggregate;
using Domain.Reservations.Repositories;
using Domain.Reservations.Services;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.Reservations.UseCases.Cancel;
internal class CancelReservationCommandHandler : ICommandHandler<CancelReservationCommand, string>
{
	
	public async Task<Result<string>> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Reservation? reservation = _reservationRepository.GetById(request.reservationId);

			if (reservation is null)
			{
				return Result.Failure<string>(new Error("if (reservation is null)", "if (reservation is null)"));
			}

			MealEntry? entry = _mealsProxy.GetMealEntry(reservation.MealEntryId);

			if (entry is null)
			{
				return Result.Failure<string>(new Error("entry is null", "entry is null"));
			}

			Customer? customer = _customersProxy.GetCustomerById(reservation.CustomerId);

			if (customer is null)
			{
				return Result.Failure<string>(new Error("if (customer is null)", "if (customer is null)"));
			}

			Reservation? firstWaitingReservation = _reservationRepository.GetFirstWaitingReservationOnEntry(entry.Id);



			if (firstWaitingReservation is not null)
			{
				_reservationsService.CancelAndGiveMealTo(reservation, firstWaitingReservation);
				//reservation.CancelAndGiveMealTo(entry, customer, firstWaitingReservation);
				_reservationRepository.Update(reservation);
				_reservationRepository.Update(firstWaitingReservation);
			}
			else
			{
				_reservationsService.Cancel(reservation);
				//reservation.Cancel(entry, customer);
				_reservationRepository.Update(reservation);
			}

			await _unitOfWork.SaveChangesAsync();

			return Result.Success(reservation.ReservationStatus);
		}

		catch (Exception exception)
		{
			return Result.Failure<string>(new Error("___", exception.Message));
		}
	}
	private readonly IUnitOfWork _unitOfWork;
	private readonly IReservationRepository _reservationRepository;
	private readonly IReservationsService _reservationsService;
	private readonly ICustomersSupDomainProxy _customersProxy;
	private readonly IMealEntriesSupDomainProxy _mealsProxy;

	public CancelReservationCommandHandler(IUnitOfWork unitOfWork,
						IReservationRepository reservationRepository,
						IReservationsService reservationsService,
						ICustomersSupDomainProxy customersProxy,
						IMealEntriesSupDomainProxy mealsProxy)
	{
		_unitOfWork = unitOfWork;
		_reservationRepository = reservationRepository;
		_reservationsService = reservationsService;
		_customersProxy = customersProxy;
		_mealsProxy = mealsProxy;
	}

}
