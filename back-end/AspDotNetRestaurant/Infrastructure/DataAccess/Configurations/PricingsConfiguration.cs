using Domain.Customers.ValueObjects;
using Domain.Meals.ValueObjects;
using Domain.Pricing.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernal.Entities;
using System.Reflection.Emit;

namespace Infrastructure.DataAccess.Configurations;
internal class PricingsConfiguration : IEntityTypeConfiguration<PricingRecord>
{
	public void Configure(EntityTypeBuilder<PricingRecord> builder)
	{
		#region Id

		builder.HasKey(record => record.Id);

		builder.HasIndex(record => record.Id).IsUnique();

		builder.Property(record => record.Id).ValueGeneratedOnAdd();

		#endregion

		#region CustomerType

		builder.Property(record => record.CustomerTypeValue);

		#endregion

		#region MealType

		builder.Property(record => record.MealTypeValue);

		#endregion

		#region MealType

		builder.Property(record => record.Price);

		#endregion


		builder.HasIndex(record => new { record.MealTypeValue, record.CustomerTypeValue}).IsUnique();
	}
}
