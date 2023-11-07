using Domain.Localization;

namespace Domain.Customers.Exceptions;

public class NegativeBalanceException : Exception
{
	public NegativeBalanceException() 
		: base(LocalizationProvider.GetResource(DomainResourcesKeys.NegativeBalanceException)) { }
}