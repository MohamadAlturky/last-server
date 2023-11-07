namespace Presentation.ApiModels;

public class CreateMealRequest
{
	public string Type { get; set; } = string.Empty;

	public int NumberOfCalories { get; set; }

	public string Name { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;

	public IFormFile ImageFile { get; set; }
}
