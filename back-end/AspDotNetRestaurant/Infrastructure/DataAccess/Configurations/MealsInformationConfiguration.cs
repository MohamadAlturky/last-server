using Domain.MealInformations.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;
internal class MealsInformationConfiguration : IEntityTypeConfiguration<MealInformation>
{
	public void Configure(EntityTypeBuilder<MealInformation> builder)
	{
		#region ID

		builder.ToTable("Meal");
		builder.HasKey(meal => meal.Id);

		builder.HasIndex(meal => meal.Id).IsUnique();

		builder.Property(meal => meal.Id).ValueGeneratedOnAdd();
		#endregion



		#region Type
		builder.Property(meal => meal.Type);
		#endregion



		#region ImagePath
		builder.Property(meal => meal.ImagePath);
		#endregion



		#region Name
		builder.Property(meal => meal.Name);
		#endregion



		#region Description
		builder.Property(meal => meal.Description);
		#endregion


		#region NumberOfCalories
		builder.Property(meal => meal.NumberOfCalories);
		#endregion
	}
}
