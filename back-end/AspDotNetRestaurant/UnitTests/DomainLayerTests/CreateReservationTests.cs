using Domain.Customers.Aggregate;
using Domain.Customers.ValueObjects;
using Domain.MealEntries.Aggregate;
using Domain.Meals.ValueObjects;
using Domain.Pricing.Aggregate;
using Domain.Reservations.Services;
using Domain.Reservations.ValueObjects;
using Xunit;

namespace UnitTests.DomainLayerTests;
public class CreateReservationTests
{

	[Fact]
	public void Create_ShouldThrowException_When_TheCustomerHasAReservation()
	{
		// Arrange
		IReservationsService service = new ReservationsService();
		var meal = new MealEntry()
		{
			AtDay = DateTime.Now,
			Id = 1,
			MealInformation = new Domain.MealInformations.Aggregate.MealInformation()
			{
				Id = 1,
				Name = "Potato"
			}
		};

		var customer = new Customer()
		{
			Id = 1,
			Balance = 1000,
			Category = CustomerType.RichPeople.ToString()
		};

		var record = new PricingRecord()
		{
			Id = 1,
			Price = 100,
			MealTypeValue = MealType.Plate.ToString(),
			CustomerTypeValue = CustomerType.RichPeople.ToString()
		};
		var isHaveAReservation = true;


		// Act and Assert
		Assert.Throws<Exception>(() =>
		{
			service.Create(meal, customer, record, 1, 1, isHaveAReservation);
		});
	}

	[Fact]
	public void Create_ShouldThrowException_When_TheCustomerDontHasEnoughMoney()
	{
		// Arrange
		IReservationsService service = new ReservationsService();
		var meal = new MealEntry()
		{
			AtDay = DateTime.Now,
			Id = 1,
			MealInformation = new Domain.MealInformations.Aggregate.MealInformation()
			{
				Id = 1,
				Name = "Potato"
			}
		};

		var customer = new Customer()
		{
			Id = 1,
			Balance = 999,
			Category = CustomerType.RichPeople.ToString()
		};

		var record = new PricingRecord()
		{
			Id = 1,
			Price = 1000,
			MealTypeValue = MealType.Plate.ToString(),
			CustomerTypeValue = CustomerType.RichPeople.ToString()
		};
		var isHaveAReservation = false;


		// Act and Assert
		Assert.Throws<Exception>(() =>
		{
			service.Create(meal, customer, record, 1, 1, isHaveAReservation);
		}).Message.Equals("you Don't have money hahahahahaha");
	}

	[Fact]
	public void Create_ShouldThrowException_When_TheDayIsBeforeToday()
	{
		// Arrange
		IReservationsService service = new ReservationsService();
		var meal = new MealEntry()
		{
			AtDay = DateTime.Now.AddDays(-1),
			Id = 1,
			MealInformation = new Domain.MealInformations.Aggregate.MealInformation()
			{
				Id = 1,
				Name = "Potato"
			}
		};

		var customer = new Customer()
		{
			Id = 1,
			Balance = 1000,
			Category = CustomerType.RichPeople.ToString()
		};

		var record = new PricingRecord()
		{
			Id = 1,
			Price = 1000,
			MealTypeValue = MealType.Plate.ToString(),
			CustomerTypeValue = CustomerType.RichPeople.ToString()
		};
		var isHaveAReservation = false;


		// Act and Assert
		Assert.Throws<Exception>(() =>
		{
			service.Create(meal, customer, record, 1, 1, isHaveAReservation);
		}).Message.Equals("entry.AtDay < Date.ToDay");
	}

	[Fact]
	public void Create_ShouldCreateNewReservationWithStatusReserved_When_EnteringTheHappyPath()
	{
		// Arrange
		IReservationsService service = new ReservationsService();
		var meal = new MealEntry()
		{
			AtDay = DateTime.Now,
			Id = 1,
			PreparedCount = 3,
			LastNumberInQueue = 1000,
			ReservationsCount = 2,
			MealInformation = new Domain.MealInformations.Aggregate.MealInformation()
			{
				Id = 1,
				Name = "Potato"
			}
		};

		var customer = new Customer()
		{
			Id = 1,
			Balance = 1000,
			Category = CustomerType.RichPeople.ToString()
		};

		var record = new PricingRecord()
		{
			Id = 1,
			Price = 1000,
			MealTypeValue = MealType.Plate.ToString(),
			CustomerTypeValue = CustomerType.RichPeople.ToString()
		};
		var isHaveAReservation = false;


		// Act
		var reservation = service.Create(meal, customer, record, 1, 1, isHaveAReservation);

		// Assert

		Assert.NotNull(reservation);
		Assert.Equal(reservation.ReservationStatus, OrderStatus.Reserved.ToString());
		Assert.Equal(0, customer.Balance);
		Assert.Equal(1001, meal.LastNumberInQueue);
		Assert.Equal(3, meal.ReservationsCount);
		Assert.Equal(record.Price,reservation.Price);
	}

	[Fact]
	public void Create_ShouldCreateNewReservationWithStatusWaiting_When_ThereIsNoMealsAvailable()
	{
		// Arrange
		IReservationsService service = new ReservationsService();
		var meal = new MealEntry()
		{
			AtDay = DateTime.Now,
			Id = 1,
			PreparedCount = 2,
			LastNumberInQueue = 1000,
			ReservationsCount = 2,
			MealInformation = new Domain.MealInformations.Aggregate.MealInformation()
			{
				Id = 1,
				Name = "Potato"
			}
		};

		var customer = new Customer()
		{
			Id = 1,
			Balance = 1000,
			Category = CustomerType.RichPeople.ToString()
		};

		var record = new PricingRecord()
		{
			Id = 1,
			Price = 1000,
			MealTypeValue = MealType.Plate.ToString(),
			CustomerTypeValue = CustomerType.RichPeople.ToString()
		};
		var isHaveAReservation = false;


		// Act
		var reservation = service.Create(meal, customer, record, 1, 1, isHaveAReservation);

		// Assert

		Assert.NotNull(reservation);
		Assert.Equal(reservation.ReservationStatus, OrderStatus.Waiting.ToString());
		Assert.Equal(1000, customer.Balance);
		Assert.Equal(1001, meal.LastNumberInQueue);
		Assert.Equal(2, meal.ReservationsCount);
		Assert.Equal(record.Price, reservation.Price);
	}
}
