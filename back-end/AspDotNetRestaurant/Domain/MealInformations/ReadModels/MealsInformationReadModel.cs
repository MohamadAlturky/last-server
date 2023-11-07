using Domain.MealInformations.Aggregate;

namespace Domain.MealInformations.ReadModels;
public class MealsInformationReadModel
{
	public int Count { get; set; }
	public List<MealInformation> MealsInformation { get; set; } = new();
}
