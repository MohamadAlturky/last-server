using Domain.Customers.Aggregate;

namespace Domain.Anticorruption;
public interface ICustomersSupDomainProxy
{
	Customer? GetCustomerById(long id);
}
