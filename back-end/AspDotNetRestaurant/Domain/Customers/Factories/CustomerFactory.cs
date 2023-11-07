using Domain.Customers.Aggregate;
using Domain.Customers.Exceptions;

namespace Domain.Customers.Factories;
public static class CustomerFactory
{
	public static Customer Create(
		int serialNumber,
		int balance,
		string firstName,
		string lastName,
		string category,
		string belongsToDepartment,
		string notes,
		bool isRegular,
		bool eligible,
		bool isActive,
		bool isSomeOneHasTheSameSerialNumber,
		long id = 0)
	{
		if (isSomeOneHasTheSameSerialNumber)
		{
			throw new SomeOneHasTheSameSerialNumberException();
		}
		return new Customer(id)
		{
			SerialNumber = serialNumber,
			Balance = balance,
			FirstName = firstName,
			LastName = lastName,
			Category = category,
			BelongsToDepartment = belongsToDepartment,
			Notes = notes,
			IsRegular = isRegular,
			Eligible = eligible,
			IsActive = isActive,
		};
	}
}
