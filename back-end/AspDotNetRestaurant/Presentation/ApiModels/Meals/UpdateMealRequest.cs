namespace Presentation.ApiModels.Meals;

public class UpdateMealRequest
{
	public long Id { get; set; }
	public string Type { get; set; } = string.Empty;
	public int NumberOfCalories { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
}
