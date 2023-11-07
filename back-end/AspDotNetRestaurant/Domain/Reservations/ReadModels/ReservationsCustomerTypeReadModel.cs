using Domain.Customers.ValueObjects;

namespace Domain.Reservations.ReadModels;
public class ReservationsCustomerTypeReadModel
{
	public string CustomerType { get; set; }
	public int NumberOfCustomers { get; set; }
}
