using Domain.MealEntries.Aggregate;

namespace Domain.Anticorruption;
public interface IMealEntriesSupDomainProxy
{
	MealEntry? GetMealEntry(long id);
}
