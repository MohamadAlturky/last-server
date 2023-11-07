namespace Domain.MealEntries.ReadModels;
public class MealEntryReadModel
{
	public long Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public int ReservationsCount { get; set; }
	public int ConsumedCount { get; set; }
	public string Day { get; set; } = string.Empty;
}
