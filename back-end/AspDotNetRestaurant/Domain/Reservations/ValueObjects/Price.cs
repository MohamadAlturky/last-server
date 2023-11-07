using Domain.Customers.Exceptions;
using Domain.Reservations.Exceptions;
using SharedKernal.ValueObjects;

namespace Domain.Reservations.ValueObjects;
public record Price : ValueObject<int>
{
	public Price(int value) : base(value)
	{
	}

	protected override void Validate(int value)
	{
		if(value < 0)
		{
			throw new NegativePriceException();
		}
	}
}
