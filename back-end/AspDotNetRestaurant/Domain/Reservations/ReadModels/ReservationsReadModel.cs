using Domain.MealEntries.Aggregate;
using Domain.MealEntries.ReadModels;

namespace Domain.Reservations.ReadModels;
public class ReservationsReadModel
{
	public MealEntryReadModel MealReadModel { get; set; } = new();
	public List<ReservationRecord> Records { get; set; } = new List<ReservationRecord>();
}
