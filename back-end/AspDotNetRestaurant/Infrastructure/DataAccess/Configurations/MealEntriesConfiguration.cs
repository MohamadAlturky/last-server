using Domain.MealEntries.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;
internal class MealEntriesConfiguration : IEntityTypeConfiguration<MealEntry>
{
	public void Configure(EntityTypeBuilder<MealEntry> builder)
	{
		#region Id
		builder.HasKey(meal => meal.Id);

		builder.HasIndex(meal => meal.Id).IsUnique();

		builder.Property(meal => meal.Id).ValueGeneratedOnAdd();
		#endregion

		builder.Property<uint>("Version").IsRowVersion().HasDefaultValue(0u);

		#region MealId
		builder.Property(meal => meal.MealInformationId);

		builder.HasIndex(meal => meal.MealInformationId);

		//builder.HasOne(meal => meal.MealsInformation).WithMany(meal=>meal.MealEntries).HasForeignKey(meal => meal.MealInformationId);

		builder.HasOne(mealEntry => mealEntry.MealInformation)
				.WithMany(meal => meal.MealEntries)
				.HasForeignKey(mealEntry => mealEntry.MealInformationId);
		#endregion

		#region AtDay

		builder.Property(meal => meal.AtDay);

		builder.HasIndex(meal => meal.AtDay);
		#endregion



		#region PreparedCount

		builder.Property(meal => meal.PreparedCount);

		#endregion



		#region LastNumberInQueue

		builder.Property(meal => meal.LastNumberInQueue);

		#endregion



		#region CustomerCanCancel

		builder.Property(meal => meal.CustomerCanCancel);

		#endregion


		#region ReservationsCount

		builder.Property(meal => meal.ReservationsCount);

		#endregion
	}
}
