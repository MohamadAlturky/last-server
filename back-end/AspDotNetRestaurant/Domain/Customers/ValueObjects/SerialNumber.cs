using Domain.Customers.Exceptions;
using SharedKernal.ValueObjects;

namespace Domain.Customers.ValueObjects;
public record SerialNumber : ValueObject<int>
{
	public SerialNumber(int value) : base(value) { }

	protected override void Validate(int value)
	{
		if(value < 0)
		{
			throw new InvalidSerialNumberException();
		}
	}
}
