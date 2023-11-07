
namespace Domain.Shared.ReadModels;
public class DailyMeals
{
	public DateOnly AtDay { get; set; }
	public List<MealReadModel> Meals { get; set; } = new List<MealReadModel>();
}
