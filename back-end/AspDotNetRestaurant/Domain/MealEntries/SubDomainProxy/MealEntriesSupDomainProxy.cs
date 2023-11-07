using Domain.Anticorruption;
using Domain.MealEntries.Aggregate;
using Domain.Meals.Repositories;

namespace Domain.MealEntries.SubDomainProxy;
public class MealEntriesSupDomainProxy: IMealEntriesSupDomainProxy
{
	private readonly IMealEntryRepository _mealRepository;

	public MealEntriesSupDomainProxy(IMealEntryRepository mealRepository)
	{
		_mealRepository = mealRepository;

	}
	public MealEntry? GetMealEntry(long id)
	{
		return _mealRepository.GetMealEntryById(id);
	}
}
