using Presentation.ApiModels.Meals;

namespace Presentation.ApiModels.MealEntry;

public class MealEntryDTO
{
	public long Id { get; set; }	
	public long MealId { get; set; }
	public bool CustomerCanCancel { get; set; } = true;
	public DateTime AtDay { get; set; } = new();
	public int ReservationsCount { get  ; set ; }
	public int PreparedCount { get; set; }
	public MealDTO? Meal { get; set; }
}
