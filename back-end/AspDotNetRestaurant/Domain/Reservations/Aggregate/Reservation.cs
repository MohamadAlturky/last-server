using Domain.Customers.Aggregate;
using Domain.MealEntries.Aggregate;
using Domain.Reservations.ValueObjects;
using SharedKernal.Entities;

namespace Domain.Reservations.Aggregate;
public class Reservation : AggregateRoot
{
	private OrderStatus _status = OrderStatus.OrderCompletedButNotRegisteredYet;
	private NumberInQueue _numberInQueue = new NumberInQueue(0);
	private Price _price = new Price(0);


	public DateTime AtDay { get; set; } = new();
	public long CustomerId { get; set; } = 0;
	public long MealEntryId { get; set; } = 0;




	public string ReservationStatus { get => _status.ToString(); set => _status = Enum.Parse<OrderStatus>(value); }
	public int NumberInQueue { get => _numberInQueue.Value; set => _numberInQueue = new NumberInQueue(value); }
	public int Price { get => _price.Value; set => _price = new Price(value); }



	public Customer? Customer { get; set; }
	public MealEntry? MealEntry { get; set; }


	// constructor
	public Reservation(long id, long customerId, long orderedMealId) : base(id)
	{
		CustomerId = customerId;
		MealEntryId = orderedMealId;
	}
	public Reservation() : base(0) { }
}
