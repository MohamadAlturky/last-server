using Domain.Customers.Aggregate;
using Domain.MealEntries.Aggregate;
using Domain.Pricing.Aggregate;
using Domain.Reservations.Aggregate;
using SharedKernal.Utilities.Result;

namespace Domain.Reservations.Services;
public interface IReservationsService
{
	(Reservation, Reservation) CreateButExchange(MealEntry entry,
		Customer customer,
		PricingRecord pricingRecord,
		long customerId,
		long orderedMealId,
		bool isCustomerHasAReservationOnThisEntry,
		Reservation firstQualifiedReservationOnWaitingToCancel);

	Reservation Create(MealEntry entry,
		Customer customer,
		PricingRecord pricingRecord,
		long customerId,
		long orderedMealId,
		bool isCustomerHasAReservationOnThisEntry);
	void CancelAndGiveMealTo(Reservation reservationToCancel, Reservation firstWaitingReservation);
	void Cancel(Reservation reservation);
	Reservation ConsumeReservation(Reservation reservation);
	void ChangeReservationsToPassed(List<Reservation> reservationsToPass);
}
