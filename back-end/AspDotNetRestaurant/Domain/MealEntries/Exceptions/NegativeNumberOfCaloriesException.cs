using Domain.Localization;

namespace Domain.MealEntries.Exceptions;
public class NegativeNumberOfCaloriesException : Exception
{
	public NegativeNumberOfCaloriesException()
		: base(LocalizationProvider
			.GetResource(DomainResourcesKeys
				.NegativeNumberOfCaloriesException))
	{ }
}
