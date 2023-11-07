using Domain.Customers.Aggregate;
using Domain.Customers.Entities;
namespace Application.Demo;

public class MealService
{
	/// <summary>
	/// "Increase the customer balance"
	/// </summary>
	/// <param name="customer"></param>
	/// <param name="balance"></param>
	/// <returns></returns>
	public List<AccountTransaction> GetAllTransactions(Customer customer)
	{
		return Database.GetAllTransactions(customer);
	}
	/// <summary>
	/// "get all account transaction"
	/// </summary>
	/// <param name="customer"></param>
	/// <param name="balance"></param>
	public void IncreaseCustomerBalance(Customer customer, int balance)
	{
		customer.IncreaseBalance(balance);

		Database.Update(customer);
	}
}













public class Database
{
	public static void Update(Customer customer)
	{

	}
	public static List<AccountTransaction> GetAllTransactions(Customer customer)
	{
		return new();
	}
}