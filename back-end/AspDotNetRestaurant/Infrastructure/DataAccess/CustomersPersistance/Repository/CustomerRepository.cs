using Application.ExecutorProvider;
using Domain.Customers.Aggregate;
using Domain.Customers.Entities;
using Domain.Customers.Exceptions;
using Domain.Customers.ReadModels;
using Domain.Customers.Repositories;
using Infrastructure.DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using SharedKernal.Entities;

namespace Infrastructure.CustomersPersistance.Repository;
public class CustomerRepository : ICustomerRepository
{
	private readonly int TRANSACTION_PAGE_SIZE = int.Parse(Properties.StaticValues.PaginationSize);
	private readonly RestaurantContext _context;
	private readonly IExecutorIdentityProvider _executorIdentityProvider;
	public CustomerRepository(RestaurantContext context, IExecutorIdentityProvider executorIdentityProvider)
	{
		_context = context;
		_executorIdentityProvider = executorIdentityProvider;
	}



	public void Add(Customer Entity)
	{
		Entity.Id = 0;

		var customer = _context.Set<Customer>().FirstOrDefault(customer => customer.SerialNumber == Entity.SerialNumber);

		if (customer is not null)
		{
			throw new SomeOneHasTheSameSerialNumberException();
		}

		_context.Set<Customer>().Add(Entity);
	}

	public void AddAccountTransaction(AccountTransaction accountTransaction)
	{
		accountTransaction.By = _executorIdentityProvider.GetExecutorSerialNumber();
		_context.Set<AccountTransaction>().Add(accountTransaction);
	}

	public long CalculateSumOfBalances()
	{
		return _context.Set<Customer>().Select(customer => customer.Balance).Sum();
	}



	public void Delete(Customer Entity)
	{
		throw new NotImplementedException();
	}

	public AccountTransactionsReadModel? GetAccountTransactionsPage(int serialNumber, int pageNumber)
	{
		Customer? customer = _context.Set<Customer>().Where(customer => customer.SerialNumber == serialNumber).FirstOrDefault();

		if (customer is null)
		{
			throw new Exception("public AccountTransactionsReadModel? GetAccountTransactionsPage(int serialNumber, int pageNumber)");
		}
		IQueryable<AccountTransaction> transacion = _context.Set<AccountTransaction>()
			.Where(entry => entry.CustomerId == customer.Id)
			.OrderByDescending(entry => entry.Id);
		int size = transacion.Count();
		var transactions = transacion
			.Skip(TRANSACTION_PAGE_SIZE * (pageNumber - 1))
			.Take(TRANSACTION_PAGE_SIZE)
			.Select(transaction => new AccountTransactionReadModel()
			{
				Type = transaction.Type,
				Value = transaction.Value,
				CreatedAtDay = transaction.CreatedAt.ToString("dddd"),
				Date = transaction.CreatedAt.ToString("dd/MM/yyyy"),
				By= transaction.By
			})
			.ToList();
		AccountTransactionsReadModel model = new AccountTransactionsReadModel()
		{
			Size = size,
			TotalBalance = customer.Balance,
			AccountTransactions = transactions,
		};
		return model;
	}

	public IEnumerable<Customer> GetAll()
	{
		return _context.Set<Customer>()
			.AsNoTracking()
			.OrderByDescending(customer => customer.Id)
			.ToList();
	}

	public Customer? GetById(long id)
	{
		return _context.Set<Customer>().Find(id);
	}

	public Customer? GetBySerialNumber(int serialNumber)
	{
		return _context.Set<Customer>()
			.Where(customer => customer.SerialNumber == serialNumber)
			.AsNoTracking()
			.SingleOrDefault();
	}

	public IEnumerable<Customer> GetPage(int pageSize, int pageNumber)
	{
		return _context.Set<Customer>()
			.OrderByDescending(customer => customer.Id)
			.AsNoTracking()
			.Skip(pageSize * (pageNumber - 1))
			.Take(pageSize).ToList();
	}

	public void Update(Customer Entity)
	{
		foreach(var transaction in Entity.AccountTransactions)
		{
			transaction.By  = _executorIdentityProvider.GetExecutorSerialNumber();
		}
		_context.Set<Customer>().Update(Entity);
	}

	public void UpdateAll(List<Customer> customers)
	{
		foreach(var customer in customers)
		{
			foreach (var transaction in customer.AccountTransactions)
			{
				transaction.By = _executorIdentityProvider.GetExecutorSerialNumber();
			}
		}
		_context.Set<Customer>().UpdateRange(customers);
	}
}
