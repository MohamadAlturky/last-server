using Infrastructure.Authentication.Models;

namespace Infrastructure.DataAccess.UserPersistence.Models;
public class CustomersPaginiationResponse
{
	public List<CustomerInformation> Customers { get; set; } = new List<CustomerInformation>();
	public int Count { get; set; }
}
