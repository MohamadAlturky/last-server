using Domain.Reservations.Exceptions;
using SharedKernal.ValueObjects;

namespace Domain.Reservations.ValueObjects;
public record NumberInQueue : ValueObject<int>
{
	public NumberInQueue(int value) : base(value) { }

	protected override void Validate(int value)
	{
		if (value < 0)
		{
			throw new NegativeNumberInQueueException();
		}
	}
}
