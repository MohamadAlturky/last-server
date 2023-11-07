using Domain.MealEntries.Aggregate;

namespace Domain.MealEntries.Factories;
public static class MealFactory
{
	public static MealEntry Create(long MealInformationId, DateTime atDay, int preparedCount,
		bool thereIsSameMealWithTheSameInformationInTheSameDay)
	{
		if (thereIsSameMealWithTheSameInformationInTheSameDay)
		{
			throw new Exception("hasAnEntryInTheSameDate");
		}
		return new MealEntry(0, MealInformationId)
		{
			AtDay = atDay,
			PreparedCount = preparedCount,
		};
	}
}
