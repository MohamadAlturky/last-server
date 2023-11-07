namespace Presentation.ApiModels.PricingRecords;

public class EditPricingRecordRequest
{
	public int Price { get; set; }
	public string CustomerTypeValue { get; set; } = string.Empty;
	public string MealTypeValue { get; set; } = string.Empty;
}
