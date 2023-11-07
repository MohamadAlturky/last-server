using Domain.MealEntries.Exceptions;
using SharedKernal.ValueObjects;

namespace Domain.Meals.ValueObjects;
public record NumberOfCalories : ValueObject<int>
{
	public NumberOfCalories(int value) : base(value)
	{
	}

	protected override void Validate(int value)
	{
		if (value < 0)
		{
			throw new NegativeNumberOfCaloriesException();
		}
	}
}
