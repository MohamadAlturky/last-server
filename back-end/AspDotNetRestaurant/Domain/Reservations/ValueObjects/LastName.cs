using SharedKernal.ValueObjects;

namespace Domain.Reservations.ValueObjects;
public record LastName : ValueObject<string>
{
	public LastName(string value) : base(value) { }

	protected override void Validate(string value)
	{
		// no validation needed
	}
}
