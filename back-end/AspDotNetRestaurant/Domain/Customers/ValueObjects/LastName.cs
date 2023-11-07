using SharedKernal.ValueObjects;

namespace Domain.Customers.ValueObjects;
public record LastName : ValueObject<string>
{
	public LastName(string value) : base(value) { }

	protected override void Validate(string value)
	{
		// no validation needed
	}
}
