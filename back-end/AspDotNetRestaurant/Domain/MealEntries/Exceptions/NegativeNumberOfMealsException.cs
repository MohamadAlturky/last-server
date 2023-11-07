using Domain.Localization;

namespace Domain.MealEntries.Exceptions;

public class NegativeNumberOfMealsException : Exception
{
	public NegativeNumberOfMealsException()
		: base(LocalizationProvider
			.GetResource(DomainResourcesKeys
				.NegativeNumberOfMealsException))
	{ }
}