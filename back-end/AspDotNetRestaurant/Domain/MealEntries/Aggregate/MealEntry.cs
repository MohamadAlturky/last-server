using Domain.MealEntries.ValueObjects;
using Domain.MealInformations.Aggregate;
using Domain.Meals.ValueObjects;
using Domain.Reservations.Aggregate;
using SharedKernal.Entities;

namespace Domain.MealEntries.Aggregate;
public class MealEntry : AggregateRoot
{

	#region Private Data Members
	private NumberOfPreparedMeals _preparedCount = new NumberOfPreparedMeals(0);
	private NumberOfReservations _reservationsCount = new NumberOfReservations(0);
	private LastNumberInQueue _lastNumberInQueue = new LastNumberInQueue(0);
	private ConsumedReservations _consumedReservations = new ConsumedReservations(0);
	#endregion

	#region Properties
	public long MealInformationId { get; set; }
	public bool CustomerCanCancel { get; set; } = true;
	public DateTime AtDay { get; set; } = new();
	public int PreparedCount { get => _preparedCount.Value; set => _preparedCount = new NumberOfPreparedMeals(value); }
	public int LastNumberInQueue { get => _lastNumberInQueue.Value; set => _lastNumberInQueue = new LastNumberInQueue(value); }
	public int ReservationsCount { get => _reservationsCount.Value; set => _reservationsCount = new NumberOfReservations(value); }
	public int ConsumedReservations { get => _consumedReservations.Value; set => _consumedReservations = new ConsumedReservations(value); }
	public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
	#endregion

	#region Navidations
	public MealInformation? MealInformation { get; set; }
	#endregion

	#region Constructors
	public MealEntry(long id, long mealInformationId) : base(id) { MealInformationId = mealInformationId; }
	public MealEntry() : base(0) { MealInformationId = 0; }
	#endregion

	#region Functionality
	public void ModifyCancellationState(bool state)
	{
		CustomerCanCancel = state;
	}

	public void AddNewReservation()
	{
		ReservationsCount++;
		LastNumberInQueue++;
	}

	public void CancelReservation()
	{
		ReservationsCount--;
	}


	#endregion
}
