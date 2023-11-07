using Infrastructure.Authentication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		//builder.HasKey(user => user.Id);

		builder.HasOne(user => user.Customer)
				   .WithOne()
				   .HasForeignKey<User>(user => user.Id)
				   .OnDelete(DeleteBehavior.ClientSetNull);
	}
}
