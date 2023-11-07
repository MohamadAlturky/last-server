using Domain.Customers.Aggregate;
using Domain.MealEntries.Aggregate;
using Domain.MealInformations.Aggregate;
using Domain.Pricing.Aggregate;
using Presentation.ApiModels.Customers;
using Presentation.ApiModels.MealEntry;
using Presentation.ApiModels.Meals;
using Presentation.ApiModels.PricingRecords;

namespace Presentation.Mappers;

public interface IMapper
{
	public MealDTO Map(MealInformation meal);
	public MealInformation Map(MealDTO meal);
	public MealEntry Map(MealEntryDTO meal);
	public MealEntryDTO Map(MealEntry meal);
	public Customer Map(CustomerDTO customerDTO);
	public CustomerDTO Map(Customer customer);
	PricingRecord Map(PricingRecordDTO recordDTO);
	PricingRecordDTO Map(PricingRecord record);
}
