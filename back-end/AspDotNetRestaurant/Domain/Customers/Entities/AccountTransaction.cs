using Domain.Customers.Aggregate;
using Domain.Customers.ValueObjects;
using SharedKernal.Entities;

namespace Domain.Customers.Entities;
public class AccountTransaction : Entity
{
	private TransactionType _transactionType;
	public AccountTransaction(long id) : base(id)
	{
	}
	public string Type
	{
		get => _transactionType.ToString(); 
		set => _transactionType = Enum.Parse<TransactionType>(value);
	}
	public int Value { get; set; }
	public string By { get; set; } = string.Empty;
	public long CustomerId { get; set; }
	public Customer Customer { get; set; }

}
