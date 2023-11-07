using Domain.MealEntries.Aggregate;

namespace Domain.Shared.ReadModels;
public static class ReadModelsMapper
{
	public static MealReadModel Map(MealEntry mealEntry)
	{
		if (mealEntry.MealInformation is null)
		{
			throw new Exception("if(mealEntry.Meal is null)");
		}

		string reservationStatus = "Empty";
		long reservationIdValue = 0;
		var reservation = mealEntry.Reservations.SingleOrDefault();

		if (reservation is not null)
		{
			reservationIdValue = reservation.Id;
			reservationStatus = reservation.ReservationStatus;
		}
		return new MealReadModel()
		{
			Id = mealEntry.Id,
			AtDay = mealEntry.AtDay,
			CustomerCanCancel = mealEntry.CustomerCanCancel,
			LastNumberInQueue = mealEntry.LastNumberInQueue,
			MealId = mealEntry.MealInformationId,
			PreparedCount = mealEntry.PreparedCount,
			ReservationsCount = mealEntry.ReservationsCount,
			Type = mealEntry.MealInformation.Type,
			NumberOfCalories = mealEntry.MealInformation.NumberOfCalories,
			Name = mealEntry.MealInformation.Name,
			ImagePath = mealEntry.MealInformation.ImagePath,
			Description = mealEntry.MealInformation.Description,
			ReservationStatus = reservationStatus,
			reservationId = reservationIdValue
		};
	}
}
