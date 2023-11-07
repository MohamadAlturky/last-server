using Domain.Localization;

namespace Domain.Reservations.Exceptions;
public class NegativeNumberOfReservationsException : Exception
{
	public NegativeNumberOfReservationsException()
		: base(LocalizationProvider
			.GetResource(DomainResourcesKeys
				.NegativeNumberOfReservationsException))
	{ }
}
