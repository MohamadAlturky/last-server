using Domain.Customers.Aggregate;

namespace Infrastructure.Authentication.Models;
public class User
{
	public User()
	{
		Roles = new HashSet<Role>();
	}
	public long Id { get; set; }
	public string HashedPassword { get; set; } = null!;
	public string HiastMail { get; set; } = string.Empty;

	public virtual Customer? Customer { get; set; }
	public virtual ICollection<Role> Roles { get; set; }
}

