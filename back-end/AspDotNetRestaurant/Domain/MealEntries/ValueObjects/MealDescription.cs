using SharedKernal.ValueObjects;

namespace Domain.Meals.ValueObjects;
internal record MealDescription : ValueObject<string>
{
	public MealDescription(string value) : base(value) { }

	protected override void Validate(string value)
	{
		// until now no validation needed
	}
}
