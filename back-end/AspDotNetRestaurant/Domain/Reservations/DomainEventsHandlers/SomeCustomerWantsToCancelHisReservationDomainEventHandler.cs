//using Domain.Reservations.Aggregate;
//using Domain.Reservations.DomainEvents;
//using Domain.Reservations.Repositories;
//using Domain.Shared.Proxies;
//using SharedKernal.DomainEvents;
//using SharedKernal.Repositories;

//namespace ReservationsServer.ReservationsDomainEventsHandlers;
//public class SomeCustomerWantsToCancelHisReservationDomainEventHandler 
//	: DomainEventHandler<SomeCustomerWantsToCancelHisReservationDomainEvent>
//{
//	private IUnitOfWork _unitOfWork;
//	private IReservationRepository _reservationRepository;

//	public SomeCustomerWantsToCancelHisReservationDomainEventHandler(IUnitOfWork unitOfWork,
//		IReservationRepository reservationRepository,
//		CustomerRepositoryProxy customerRepositoryProxy, 
//		MealRepositoryProxy mealRepositoryProxy, 
//		PricingRepositoryProxy pricingRepositoryProxy)
//	{
//		_unitOfWork = unitOfWork;
//		_reservationRepository = reservationRepository;
//	}

	
//	public async Task Handle(SomeCustomerWantsToCancelHisReservationDomainEvent notification, CancellationToken cancellationToken)
//	{

//		List<Reservation> reservationsToCancel = _reservationRepository
//			.GetOnWaitingToCancel(notification.mealEntryId)
//			.OrderBy(reservation => reservation.NumberInQueue).ToList();

//		List<Reservation> reservationsToAccept = _reservationRepository
//			.GetTheKFirstWaitingReservationsOnEntry(notification.mealEntryId,
//			reservationsToCancel.Count)
//			.OrderBy(reservation => reservation.NumberInQueue).ToList();

//		for (int index = 0; index < reservationsToCancel.Count && index < reservationsToAccept.Count; index++)
//		{
//			reservationsToAccept[index].AcceptOnPromise();
//			reservationsToCancel[index].CancelOnPromise();
//		}
//		await _unitOfWork.SaveChangesAsync();
//	}
//}
