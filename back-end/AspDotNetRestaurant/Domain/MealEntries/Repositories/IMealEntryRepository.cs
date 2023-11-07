using SharedKernal.Repositories;
using Domain.Meals.ValueObjects;
using Domain.Shared.ReadModels;
using Domain.MealInformations.Aggregate;
using Domain.MealEntries.Aggregate;

namespace Domain.Meals.Repositories;

public interface IMealEntryRepository : IRepository<MealEntry>
{
	IEnumerable<MealEntry> GetMealEntries(long mealId);
	MealEntry? GetMealEntryById(long entryId);
	List<MealEntry> GetEntriesByDate(DateOnly date);
	List<AutoCompleteModel> GetAutoComplete(string partOfMealName, MealType mealType);
	List<MealInformation> GetEntriesByNameAndType(string mealName, MealType type);
	WeeklyPreparedMeals GetWeeklyMealsStartsFrom(DateTime startOfTheWeek,long customerId);
	void Add(MealEntry newMeal);
	MealEntry? GetMealEntryWithAllInformationAboutReservationsAndCustomers(long mealEntryId);
	void Delete(MealEntry fullMealEntry);
	WeeklyPreparedMeals GetMealsScheduleStartsFrom(DateTime startOfTheWeek);
}
