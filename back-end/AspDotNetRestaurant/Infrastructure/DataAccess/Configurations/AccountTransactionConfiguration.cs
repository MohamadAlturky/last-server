using Domain.Customers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;
internal class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
{
	public void Configure(EntityTypeBuilder<AccountTransaction> builder)
	{
	}
}
