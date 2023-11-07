namespace Presentation.ApiModels;

public class GetMealsByNameAndTypeRequest
{
	public string mealName { get; set; } = string.Empty;
	public string mealType { get; set; } = string.Empty;
}