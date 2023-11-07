using Domain.MealEntries.Exceptions;
using SharedKernal.ValueObjects;

namespace Domain.Meals.ValueObjects;
public record NumberOfPreparedMeals : ValueObject<int>
{
	public NumberOfPreparedMeals(int value) : base(value)
	{ }

	protected override void Validate(int value)
	{
		if (value < 0)
		{
			throw new NegativeNumberOfMealsException();
		}
	}
}
