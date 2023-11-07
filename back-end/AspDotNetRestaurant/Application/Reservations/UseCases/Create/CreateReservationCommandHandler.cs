using Application.Reservations.UseCases.Cancel;
using Domain.Anticorruption;
using Domain.Customers.Aggregate;
using Domain.Localization;
using Domain.MealEntries.Aggregate;
using Domain.Pricing.Aggregate;
using Domain.Reservations.Aggregate;
using Domain.Reservations.Repositories;
using Domain.Reservations.Services;
using Microsoft.Extensions.Logging;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;
using System;
using System.Data;

namespace Application.Reservations.UseCases.Create;
internal class CreateReservationCommandHandler : ICommandHandler<CreateReservationCommand, CreateReservationResponse>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IReservationRepository _reservationRepository;
	private readonly IReservationsService _reservationsService;
	private readonly ICustomersSupDomainProxy _customersProxy;
	private readonly IMealEntriesSupDomainProxy _mealsProxy;
	private readonly IPricingRecordsSupDomainProxy _pricingRecordsProxy;

	public CreateReservationCommandHandler(IUnitOfWork unitOfWork,
						IReservationRepository reservationRepository,
						IReservationsService reservationsService,
						ICustomersSupDomainProxy customersProxy,
						IMealEntriesSupDomainProxy mealsProxy,
						IPricingRecordsSupDomainProxy pricingRecordsProxy)
	{
		_unitOfWork = unitOfWork;
		_reservationRepository = reservationRepository;
		_reservationsService = reservationsService;
		_customersProxy = customersProxy;
		_mealsProxy = mealsProxy;
		_pricingRecordsProxy = pricingRecordsProxy;
	}

	public async Task<Result<CreateReservationResponse>> Handle(CreateReservationCommand request,
		CancellationToken cancellationToken)
	{
		try
		{
			MealEntry? entry = _mealsProxy.GetMealEntry(request.orderedMealId);

			if (entry is null || entry.MealInformation is null)
			{
				return Result.Failure<CreateReservationResponse>(new Error("entry.Meal is null", "entry.Meal is null"));
			}

			Customer? customer = _customersProxy.GetCustomerById(request.customerId);

			if (customer is null)
			{
				return Result.Failure<CreateReservationResponse>(new Error("(customer is null)", "(customer is null)"));
			}

			PricingRecord? pricingRecord = _pricingRecordsProxy
				.GetPriceByCustomerTypeJoinMealType(customer.Category, entry.MealInformation.Type);


			if (pricingRecord is null)
			{
				return Result.Failure<CreateReservationResponse>(new Error("(pricingRecord is null)", "(pricingRecord is null)"));
			}

			bool isCustomerHasAReservationOnThisEntry =
				_reservationRepository.CheckIfCustomerHasAMealReservation(request.customerId,
						request.orderedMealId);

			Reservation? firstQualifiedReservationOnWaitingToCancel =
				_reservationRepository.GetFirstOnWaitingToCancelWhereHisBalanceIsEnough(entry.Id);


			Reservation reservation;
			if (firstQualifiedReservationOnWaitingToCancel is null)
			{
				reservation = _reservationsService.Create(entry, customer,
												 pricingRecord, request.customerId,
												 request.orderedMealId,
												 isCustomerHasAReservationOnThisEntry);
			}
			else
			{
				(Reservation, Reservation) exchangableReservations = _reservationsService.CreateButExchange(entry, customer,
											 pricingRecord, request.customerId,
											 request.orderedMealId,
											 isCustomerHasAReservationOnThisEntry,
											 firstQualifiedReservationOnWaitingToCancel);

				reservation = exchangableReservations.Item1;

				firstQualifiedReservationOnWaitingToCancel = exchangableReservations.Item2;

				_reservationRepository.Update(firstQualifiedReservationOnWaitingToCancel);
			}

			_reservationRepository.Add(reservation);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success(new CreateReservationResponse(reservation.ReservationStatus, reservation.Id));
		}
		catch (Exception exception)
		{
			return Result.Failure<CreateReservationResponse>(new Error("", exception.Message));
		}
	}
}
