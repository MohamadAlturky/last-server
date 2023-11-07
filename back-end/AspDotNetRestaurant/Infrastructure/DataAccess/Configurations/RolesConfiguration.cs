using Infrastructure.Authentication.Models;
using Infrastructure.Authorization.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;
internal class RolesConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.HasIndex(role => role.Id);

		builder.HasMany(role => role.Permissions)
			.WithMany(permission => permission.Roles)
			.UsingEntity<RolePermission>();

		builder.HasMany(role => role.Users)
			.WithMany(user => user.Roles)
			.UsingEntity<UserRole>();

		IEnumerable<Role> roles = new List<Role>()
		{
			RolesDictionary.User,
			RolesDictionary.Manager,
			RolesDictionary.Consumer,
			RolesDictionary.Accountant
		};
		builder.HasData(roles);
	}
}

