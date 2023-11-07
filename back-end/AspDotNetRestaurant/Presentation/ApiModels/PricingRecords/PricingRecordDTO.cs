
namespace Presentation.ApiModels.PricingRecords;

public class PricingRecordDTO
{
	public long Id { get; set; }
	public int Price { get; set; }
	public string CustomerType { get; set; } = string.Empty;
	public string MealType { get; set; } = string.Empty;
}
