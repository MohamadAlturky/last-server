//using Domain.Reservations.DomainEvents;
//using Domain.Reservations.Repositories;
//using Domain.Reservations.ValueObjects;
//using Domain.Shared.Entities;
//using Domain.Shared.Proxies;
//using SharedKernal.DomainEvents;
//using SharedKernal.Repositories;

//namespace ReservationsServer.ReservationsDomainEventsHandlers;
//public class SomeCustomerCanceledHisReservationDomainEventHandler
//	: DomainEventHandler<SomeCustomerCanceledHisReservationDomainEvent>
//{

//	private IUnitOfWork _unitOfWork;
//	private IReservationRepository _reservationRepository;
//	private MealRepositoryProxy _mealRepositoryProxy;
//	private PricingRepositoryProxy _pricingRepositoryProxy;

//	public SomeCustomerCanceledHisReservationDomainEventHandler(IUnitOfWork unitOfWork,
//		IReservationRepository reservationRepository,
//		CustomerRepositoryProxy customerRepositoryProxy,
//		MealRepositoryProxy mealRepositoryProxy,
//		PricingRepositoryProxy pricingRepositoryProxy)
//	{
//		_unitOfWork = unitOfWork;
//		_reservationRepository = reservationRepository;
//		_mealRepositoryProxy = mealRepositoryProxy;
//		_pricingRepositoryProxy = pricingRepositoryProxy;
//	}

//	public async Task Handle(SomeCustomerCanceledHisReservationDomainEvent notification, CancellationToken cancellationToken)
//	{
//		MealEntry? mealEntry = _mealRepositoryProxy.GetMealEntryById(notification.mealEntryId);

//		if (mealEntry is null)
//		{
//			throw new Exception("if(mealEntry is null)");
//		}
//		if (mealEntry.MealsInformation is null)
//		{
//			throw new Exception("mealEntry.MealsInformation is null");
//		}

//		var availableMeals = mealEntry.PreparedCount - mealEntry.ReservationsCount;

//		var resevationsToRegister =
//			_reservationRepository.GetTheKFirstWaitingReservationsOnEntry(notification.mealEntryId, availableMeals);

//		foreach (var reservation in resevationsToRegister)
//		{
//			mealEntry.ReservationsCount++;
//			reservation.ReservationStatus = OrderStatus.Reserved.ToString();

//			if (reservation.Customer is null)
//			{
//				throw new Exception("if(reservation.Customer is null)");
//			}

//			PricingRecord? price =
//				_pricingRepositoryProxy
//				.GetPriceByCustomerTypeJoinMealType
//				(reservation.Customer.Category, mealEntry.MealsInformation.Type);

//			if (price is null)
//			{
//				throw new Exception("if (price is null)");
//			}


//			reservation.Customer.DecreaseBalance(price.Price);
//		}

//		await _unitOfWork.SaveChangesAsync();
//	}
//}
