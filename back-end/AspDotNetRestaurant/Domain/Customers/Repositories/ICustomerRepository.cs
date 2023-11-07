using Domain.Customers.Aggregate;
using Domain.Customers.Entities;
using Domain.Customers.ReadModels;
using SharedKernal.Repositories;

namespace Domain.Customers.Repositories;
public interface ICustomerRepository : IRepository<Customer>
{

	IEnumerable<Customer> GetAll();
	IEnumerable<Customer> GetPage(int pageSize, int pageNumber);
	Customer? GetById(long id);


	void Add(Customer Entity);
	void Update(Customer Entity);
	void Delete(Customer Entity);

	Customer? GetBySerialNumber(int serialNumber);
	long CalculateSumOfBalances();
	void UpdateAll(List<Customer> customers);
	void AddAccountTransaction(AccountTransaction accountTransaction);
	AccountTransactionsReadModel? GetAccountTransactionsPage(int serialNumber, int pageNumber);
}
