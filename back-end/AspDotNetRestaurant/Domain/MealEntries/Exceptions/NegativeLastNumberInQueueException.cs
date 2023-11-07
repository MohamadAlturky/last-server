using Domain.Localization;

namespace Domain.MealEntries.Exceptions;
public class NegativeLastNumberInQueueException : Exception
{
	public NegativeLastNumberInQueueException()
		: base(LocalizationProvider
			.GetResource(DomainResourcesKeys
				.NegativeLastNumberInQueueException))
	{ }
}
