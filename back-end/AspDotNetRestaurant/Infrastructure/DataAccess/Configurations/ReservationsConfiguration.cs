using Domain.Reservations.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;
internal class ReservationsConfiguration : IEntityTypeConfiguration<Reservation>
{
	public void Configure(EntityTypeBuilder<Reservation> builder)
	{
		#region ID

		builder.HasKey(reservation => reservation.Id);

		builder.HasIndex(reservation => reservation.Id).IsUnique();

		builder.Property(reservation => reservation.Id).ValueGeneratedOnAdd();



		#endregion





		#region CustomerId

		builder.Property(reservation => reservation.CustomerId);

		builder.HasOne(reservation => reservation.Customer)
			.WithMany(customer=>customer.Reservations)
			.HasForeignKey(reservation => reservation.CustomerId);
		#endregion


		#region MealId

		builder.Property(reservation => reservation.MealEntryId);

		builder.HasOne(reservation => reservation.MealEntry)
			.WithMany(mealEntry=>mealEntry.Reservations)
			.HasForeignKey(reservation => reservation.MealEntryId);
		#endregion




		#region Status

		builder.Property(reservation => reservation.ReservationStatus);
		#endregion


		#region AtDay
		builder.Property(reservation => reservation.AtDay);

		builder.HasIndex(reservation => reservation.AtDay);
		#endregion




		#region NumberInQueue
		builder.Property(reservation => reservation.NumberInQueue);
		#endregion




		#region Price

		builder.Property(reservation => reservation.Price);
		#endregion
	}
}
