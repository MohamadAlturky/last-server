using Domain.Customers.Aggregate;
using Domain.MealEntries.Aggregate;
using Domain.Pricing.Aggregate;
using Domain.Reservations.Aggregate;
using Domain.Reservations.ValueObjects;
using Domain.Shared.Utilities;
using SharedKernal.Utilities.Result;

namespace Domain.Reservations.Services;
public class ReservationsService: IReservationsService
{
	public ReservationsService() { }

	public void Cancel(Reservation reservation)
	{
		CheckCancellationValidity(reservation);

		if (Date.ToDay == reservation.MealEntry?.AtDay)
		{
			reservation.ReservationStatus = OrderStatus.OnTheCanceledListButNotCanceledYet.ToString();

		}
		else if (Date.ToDay < reservation.MealEntry?.AtDay)
		{
			reservation.ReservationStatus = OrderStatus.Canceled.ToString();

			reservation.Customer?.IncreaseBalance(reservation.Price);

			reservation.MealEntry.ReservationsCount--;
		}
	}

	public void CancelAndGiveMealTo(Reservation reservationToCancel, Reservation firstWaitingReservation)
	{
		CheckCancellationValidity(reservationToCancel);

		reservationToCancel.ReservationStatus = OrderStatus.Canceled.ToString();

		reservationToCancel.Customer?.IncreaseBalance(reservationToCancel.Price);

		if (firstWaitingReservation.Customer is null)
		{
			throw new Exception("if (firstWaitingReservation.Customer is null)");
		}

		firstWaitingReservation.Customer.DecreaseBalance(firstWaitingReservation.Price);

		firstWaitingReservation.ReservationStatus = OrderStatus.Reserved.ToString();
	}

	public void ChangeReservationsToPassed(List<Reservation> reservationsToPass)
	{
		for(int i = 0; i < reservationsToPass.Count; i++)
		{
			reservationsToPass[i].ReservationStatus=OrderStatus.Passed.ToString();
		}
	}

	public Reservation ConsumeReservation(Reservation reservation)
	{
		if(reservation.ReservationStatus != OrderStatus.Reserved.ToString())
		{
			throw new Exception("if(reservation.ReservationStatus != OrderStatus.Reserved.ToString())");
		}

		reservation.ReservationStatus = OrderStatus.Consumed.ToString();
		reservation.MealEntry.ConsumedReservations++;
		return reservation;
	}

	public Reservation Create(MealEntry entry, Customer customer, PricingRecord pricingRecord,
			long customerId, long orderedMealId, bool isCustomerHasAReservationOnThisEntry)
	{
		if (isCustomerHasAReservationOnThisEntry)
		{
			throw new Exception("CustomerHasAReservationOnThisEntry");
		}

		Reservation reservation = new Reservation(0, customerId, orderedMealId);

		reservation.MealEntry = entry;

		reservation.Customer = customer;

		if (customer.Balance < pricingRecord.Price)
		{
			throw new Exception("you Don't have money hahahahahaha");
		}

		if (entry.AtDay < Date.ToDay)
		{
			throw new Exception("entry.AtDay < Date.ToDay");
		}

		entry.LastNumberInQueue++;

		reservation.NumberInQueue = entry.LastNumberInQueue;
		reservation.AtDay = entry.AtDay;
		reservation.Price = pricingRecord.Price;

		#pragma Edited When Testing was entry.ReservationsCount > entry.PreparedCount
		
		if (entry.ReservationsCount >= entry.PreparedCount)
		{
			reservation.ReservationStatus = OrderStatus.Waiting.ToString();
			//reservation.Raise(new SomeCustomerNewInTheWaitingListDomainEvent(entry.Id));
		}
		else
		{
			entry.ReservationsCount++;
			customer.DecreaseBalance(pricingRecord.Price);
			reservation.ReservationStatus = OrderStatus.Reserved.ToString();
		}

		return reservation;
	}


	public (Reservation, Reservation) CreateButExchange(MealEntry entry,
		Customer customer,
		PricingRecord pricingRecord,
		long customerId,
		long orderedMealId,
		bool isCustomerHasAReservationOnThisEntry,
		Reservation firstQualifiedReservationOnWaitingToCancel)
	{
		if (isCustomerHasAReservationOnThisEntry)
		{
			throw new Exception("CustomerHasAReservationOnThisEntry");
		}

		Reservation reservation = new Reservation(0, customerId, orderedMealId);

		reservation.MealEntry = entry;

		reservation.Customer = customer;

		if (customer.Balance < pricingRecord.Price)
		{
			throw new Exception("you Don't have money hahahahahaha");
		}

		if (entry.AtDay < Date.ToDay)
		{
			throw new Exception("entry.AtDay < Date.ToDay");
		}

		entry.LastNumberInQueue++;

		reservation.NumberInQueue = entry.LastNumberInQueue;
		reservation.AtDay = entry.AtDay;
		reservation.Price = pricingRecord.Price;


		customer.DecreaseBalance(pricingRecord.Price);
		reservation.ReservationStatus = OrderStatus.Reserved.ToString();

		firstQualifiedReservationOnWaitingToCancel.
			Customer?
			.IncreaseBalance(firstQualifiedReservationOnWaitingToCancel.Price);

		firstQualifiedReservationOnWaitingToCancel.ReservationStatus = OrderStatus.Canceled.ToString();

		return (reservation, firstQualifiedReservationOnWaitingToCancel);
	}

	private void CheckCancellationValidity(Reservation reservation)
	{
		if (reservation.MealEntry is null)
		{
			throw new Exception("if(MealEntry is null)");
		}
		if (reservation.ReservationStatus == OrderStatus.Canceled.ToString())
		{
			throw new Exception("if (ReservationStatus == OrderStatus.Canceled.ToString())");
		}
		if (!reservation.MealEntry.CustomerCanCancel)
		{
			throw new Exception("!MealEntry.CustomerCanCancel");
		}

		if (Date.ToDay > reservation.MealEntry.AtDay)
		{
			throw new Exception("if(today > entry.AtDay)");
		}

		if (reservation.ReservationStatus == OrderStatus.Consumed.ToString())
		{
			throw new Exception("if(this.ReservationStatus == OrderStatus.مستهلكة.ToString())");
		}
	}
}
