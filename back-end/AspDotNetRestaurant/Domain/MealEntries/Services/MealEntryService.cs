using Domain.MealEntries.Aggregate;
using Domain.Reservations.ValueObjects;

namespace Domain.MealEntries.Services;
public class MealEntryService : IMealEntryService
{
	public void ReturnMoneyForCutomers(MealEntry mealEntry)
	{
		foreach (var reservation in mealEntry.Reservations)
		{
			if (reservation.Customer is null)
			{
				throw new Exception("reservation.Customer is null");
			}
			if (reservation.ReservationStatus != OrderStatus.Consumed.ToString() &&
			   reservation.ReservationStatus != OrderStatus.Canceled.ToString() &&
			   reservation.ReservationStatus != OrderStatus.Waiting.ToString())
			{

				reservation.Customer?.IncreaseBalance(reservation.Price);
			}
		}
	}
}
