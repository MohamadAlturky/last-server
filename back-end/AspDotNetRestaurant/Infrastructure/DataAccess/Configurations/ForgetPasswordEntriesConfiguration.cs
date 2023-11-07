using Infrastructure.ForgetPasswordHandling.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;
internal class ForgetPasswordEntriesConfiguration
	: IEntityTypeConfiguration<ForgetPasswordEntry>
{
	public void Configure(EntityTypeBuilder<ForgetPasswordEntry> builder)
	{
		
	}
}
