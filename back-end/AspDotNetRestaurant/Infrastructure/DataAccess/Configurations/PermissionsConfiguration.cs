using Infrastructure.Authentication.Models;
using Infrastructure.Authorization.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;
public class PermissionsConfiguration : IEntityTypeConfiguration<Permission>
{
	public void Configure(EntityTypeBuilder<Permission> builder)
	{
		builder.HasKey(permission => permission.Id);

		IEnumerable<Permission> permissions = new List<Permission>()
		{
			PermissionsDictionary.RegisterCustomer,
			PermissionsDictionary.OrderContent,
			PermissionsDictionary.CreateSystemInformation,
			PermissionsDictionary.ReadContent,
			PermissionsDictionary.ReadSystemInformation,
			PermissionsDictionary.ConsumeReservations,
			PermissionsDictionary.EditBalances,
			PermissionsDictionary.SeePublicContent
		};

		builder.HasData(permissions);
	}
}
