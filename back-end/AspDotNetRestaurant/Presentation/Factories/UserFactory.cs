using Domain.Customers.Aggregate;
using Infrastructure.Authentication.Models;
using Presentation.ApiModels;
using Presentation.ApiModels.Register;

namespace Presentation.Factories;

public static class UserFactory
{
	public static User Create(RegistrationModel model)
	{
		return new User()
		{
			Id = 0,
			Customer = new Customer()
			{
				Id = 0,
				SerialNumber = model.SerialNumber,
				BelongsToDepartment = model.BelongsToDepartment,
				Balance = 0,
				Category = model.Category,
				Eligible = model.Eligible,
				FirstName = model.FirstName,
				LastName = model.LastName,
				IsRegular = model.IsRegular,
				Notes = model.Notes,
				IsActive=true
			},
			HiastMail = model.HiastMail,
			Roles = new List<Role>()
			{
				new Role()
				{
					Id = 2
				}
			}
			//Roles = model.Roles.Select(id => new Role()
			//{
			//	Id = id
			//}).ToList()
		};
	}
	public static User Create(EditCustomerInformationRequest model)
	{
		return new User()
		{
			Id = 0,
			Customer = new Customer()
			{
				Id = 0,
				SerialNumber = model.SerialNumber,
				Balance = 0,
				Category = model.Category,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Notes = model.Notes,
			},
			HiastMail = model.HiastMail,
		};
	}
}
