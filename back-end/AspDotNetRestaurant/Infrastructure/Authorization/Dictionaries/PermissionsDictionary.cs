using Infrastructure.Authentication.Models;

namespace Infrastructure.Authorization.Dictionaries;
public static class PermissionsDictionary
{
	public static Permission ReadContent => new()
	{
		Id = 1,
		Name = "ReadContent"
	};

	public static Permission RegisterCustomer => new()
	{
		Id = 2,
		Name = "RegisterCustomer"
	};

	public static Permission CreateSystemInformation => new()
	{
		Id = 3,
		Name = "CreateSystemInformation"
	};

	public static Permission OrderContent => new()
	{
		Id = 4,
		Name = "OrderContent"
	};

	public static Permission ReadSystemInformation => new()
	{
		Id = 5,
		Name = "ReadSystemInformation"
	};

	public static Permission ConsumeReservations => new()
	{
		Id = 6,
		Name = "ConsumeReservations"
	};

	public static Permission EditBalances => new()
	{
		Id = 7,
		Name = "EditBalances"
	};
	public static Permission SeePublicContent => new()
	{
		Id = 8,
		Name = "SeePublicContent"
	};
}
