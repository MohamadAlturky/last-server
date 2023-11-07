using Domain.Reservations.Exceptions;
using SharedKernal.ValueObjects;

namespace Domain.Meals.ValueObjects;
public record NumberOfReservations : ValueObject<int>
{
	public NumberOfReservations(int value) : base(value) { }

	protected override void Validate(int value)
	{
		if (value < 0)
		{
			throw new NegativeNumberOfReservationsException();
		}
	}
}
