using Domain.Customers.Aggregate;
using Domain.Customers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;
internal class CustomersConfiguration : IEntityTypeConfiguration<Customer>
{
	public void Configure(EntityTypeBuilder<Customer> builder)
	{
		#region ID
		builder.HasKey(customer => customer.Id);

		builder.HasIndex(customer => customer.Id).IsUnique();

		builder.Property(customer => customer.Id);
		#endregion


		#region SerialNumber

		builder.HasIndex(customer => customer.SerialNumber).IsUnique();

		builder.Property(customer => customer.SerialNumber);
		#endregion



		#region Balance

		builder.Property(customer => customer.Balance);

		#endregion


		#region Type

		builder.Property(customer => customer.Category);

		#endregion


		#region FirstName
		builder.Property(customer => customer.FirstName);
		#endregion

		#region LastName
		builder.Property(customer => customer.LastName);
		#endregion

		#region IsActive
		builder.Property(customer => customer.IsActive);
		#endregion

		#region Eligible
		builder.Property(customer => customer.Eligible);
		#endregion

		#region IsRegular
		builder.Property(customer => customer.IsRegular);
		#endregion

		#region Notes
		builder.Property(customer => customer.Notes);
		#endregion

		#region BelongsToDepartment
		builder.Property(customer => customer.BelongsToDepartment);
		#endregion



	}

}
