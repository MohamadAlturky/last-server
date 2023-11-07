using SharedKernal.ValueObjects;

namespace Domain.MealEntries.ValueObjects;
public record ConsumedReservations : ValueObject<int>
{
	public ConsumedReservations(int original) : base(original)
	{
	}

	protected override void Validate(int value)
	{
		if(value < 0)
		{
			throw new Exception("if(value < 0)");
		}
	}
}
