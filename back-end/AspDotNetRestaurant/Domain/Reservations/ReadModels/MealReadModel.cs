using Domain.Meals.ValueObjects;
namespace Domain.Shared.ReadModels;

public class MealReadModel
{
	public long Id { get; set; }
	public long MealId { get; set; }
	public int PreparedCount { get; set; }
	public int LastNumberInQueue { get; set; }
	public int ReservationsCount { get; set; }
	public int NumberOfCalories { get; set; }

	public string ReservationStatus { get; set; } = string.Empty;
	public long reservationId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Type { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string ImagePath { get; set; } = string.Empty;
	
	public bool CustomerCanCancel { get; set; } = true;
	public DateTime AtDay { get; set; }
}