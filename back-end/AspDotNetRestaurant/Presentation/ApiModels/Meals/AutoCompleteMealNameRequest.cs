namespace Presentation.ApiModels.Meals;

public class AutoCompleteMealNameRequest
{
	public string PartOfMealName { get; set; } = string.Empty;
	public string MealType { get; set; } = string.Empty;
}
