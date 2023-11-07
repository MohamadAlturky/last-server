using Domain.Anticorruption;
using Domain.Customers.Aggregate;
using Domain.Customers.Repositories;

namespace Domain.Customers.SupDomainProxy;
public class CustomersSupDomainProxy : ICustomersSupDomainProxy
{
	private readonly ICustomerRepository _customerRepository;

	public CustomersSupDomainProxy(ICustomerRepository customerRepository)
	{
		_customerRepository = customerRepository;
	}
	public Customer? GetCustomerById(long id)
	{
		return _customerRepository.GetById(id);
	}
}
