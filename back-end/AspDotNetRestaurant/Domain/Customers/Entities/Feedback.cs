using Domain.Customers.Aggregate;
using SharedKernal.Entities;

namespace Domain.Customers.Entities;
public class Feedback : Entity
{
	public Feedback(long id) : base(id)
	{
	}
	public Feedback() : base(0)
	{
	}
	public long CustomerId { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;

	public Customer Customer { get; set; }
}
