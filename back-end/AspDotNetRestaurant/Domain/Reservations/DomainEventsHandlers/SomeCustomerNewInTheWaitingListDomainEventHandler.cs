//using Domain.Reservations.Aggregate;
//using Domain.Reservations.DomainEvents;
//using Domain.Reservations.Repositories;
//using SharedKernal.DomainEvents;
//using SharedKernal.Repositories;

//namespace Domain.Reservations.DomainEventsHandlers;
//internal class SomeCustomerNewInTheWaitingListDomainEventHandler : DomainEventHandler<SomeCustomerNewInTheWaitingListDomainEvent>
//{
//	private IUnitOfWork _unitOfWork;
//	private IReservationRepository _reservationRepository;

//	public SomeCustomerNewInTheWaitingListDomainEventHandler(IUnitOfWork unitOfWork, 
//		IReservationRepository reservationRepository)
//	{
//		_unitOfWork = unitOfWork;
//		_reservationRepository = reservationRepository;
//	}

//	public async Task Handle(SomeCustomerNewInTheWaitingListDomainEvent notification, CancellationToken cancellationToken)
//	{

//		List<Reservation> reservationsToCancel =_reservationRepository
//			.GetOnWaitingToCancel(notification.mealEntryId)
//			.OrderBy(reservation=>reservation.NumberInQueue).ToList();

//		List<Reservation> reservationsToAccept = _reservationRepository
//			.GetTheKFirstWaitingReservationsOnEntry(notification.mealEntryId,
//			reservationsToCancel.Count)
//			.OrderBy(reservation => reservation.NumberInQueue).ToList();

//		for(int index = 0;index < reservationsToCancel.Count && index < reservationsToAccept.Count; index++)
//		{
//			reservationsToAccept[index].AcceptOnPromise();
//			reservationsToCancel[index].CancelOnPromise();
//		}
//		await _unitOfWork.SaveChangesAsync();
//	}
//}
