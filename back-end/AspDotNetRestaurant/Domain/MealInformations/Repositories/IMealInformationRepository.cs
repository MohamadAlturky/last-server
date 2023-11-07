using Domain.MealEntries.Aggregate;
using Domain.MealInformations.Aggregate;
using Domain.MealInformations.ReadModels;
using SharedKernal.Repositories;

namespace Domain.MealInformations.Repositories;
public interface IMealInformationRepository : IRepository<MealInformation>
{
	IEnumerable<MealInformation> GetAllInformations();
	IEnumerable<MealInformation> GetInformationsPage(int pageSize, int pageNumber);
	
	MealInformation? GetInformationById(long id);
	MealInformation? GetMealWithEntry(long id, Func<MealEntry, bool> entrySelector);

	void AddInformation(MealInformation Entity);
	void UpdateInformation(MealInformation Entity);
	void DeleteInformation(MealInformation Entity);

	int GetNumberOfRecordsForPaginiation();
	bool CheckIfMealHasEntryInDay(long mealId, DateOnly day);
	MealsInformationReadModel GetMealsInformationPage(int pageNumber);
	bool IsThereAnyEntry(long mealId);
}
