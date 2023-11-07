namespace Presentation.ApiModels.MealEntry;

public class PrepareRequest
{
    public	long mealId { get; set; }
	public string atDay { get; set; } = string.Empty;
	public int numberOfUnits { get; set; }
}
