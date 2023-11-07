using Domain.Customers.Entities;

namespace Domain.Customers.ReadModels;
public class AccountTransactionsReadModel
{
	public long Size { get; set; }
	public long TotalBalance { get; set; }
	public List<AccountTransactionReadModel> AccountTransactions { get; set; } = new List<AccountTransactionReadModel>();
}
