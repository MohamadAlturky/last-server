
using Domain.MealEntries.Aggregate;
using SharedKernal.Utilities.Result;

namespace Domain.MealEntries.Services;
public interface IMealEntryService
{
	void ReturnMoneyForCutomers(MealEntry mealEntry);
}
