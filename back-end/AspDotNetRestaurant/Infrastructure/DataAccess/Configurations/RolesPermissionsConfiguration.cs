using Infrastructure.Authentication.Models;
using Infrastructure.Authorization.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;
public class RolesPermissionsConfiguration : IEntityTypeConfiguration<RolePermission>
{
	public void Configure(EntityTypeBuilder<RolePermission> builder)
	{
		builder.HasKey(entity => new { entity.RoleId, entity.PermissionId });

		IEnumerable<RolePermission> rolePermissions = new List<RolePermission>()
		{
			Create(RolesDictionary.Manager,PermissionsDictionary.RegisterCustomer),
			Create(RolesDictionary.Manager,PermissionsDictionary.SeePublicContent),
			Create(RolesDictionary.Manager,PermissionsDictionary.CreateSystemInformation),
			Create(RolesDictionary.Manager,PermissionsDictionary.ReadSystemInformation),
			Create(RolesDictionary.Manager,PermissionsDictionary.EditBalances),

			Create(RolesDictionary.Consumer,PermissionsDictionary.ConsumeReservations),
			Create(RolesDictionary.Consumer,PermissionsDictionary.SeePublicContent),
			
			Create(RolesDictionary.Accountant,PermissionsDictionary.EditBalances),
			Create(RolesDictionary.Accountant,PermissionsDictionary.SeePublicContent),

			Create(RolesDictionary.User,PermissionsDictionary.ReadContent),
			Create(RolesDictionary.User,PermissionsDictionary.SeePublicContent),
			Create(RolesDictionary.User,PermissionsDictionary.OrderContent)
		};

		builder.HasData(rolePermissions);
	}
	private static RolePermission Create(Role role, Permission permission)
	{
		return new RolePermission()
		{
			PermissionId = permission.Id,
			RoleId = role.Id
		};
	}
}
