using Domain.MealEntries.Aggregate;
using Domain.MealEntries.ReadModels;
using Domain.Reservations.Aggregate;
using Domain.Reservations.ReadModels;
using Domain.Reservations.Repositories;
using Domain.Reservations.ValueObjects;
using Infrastructure.DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ReservationsPersistence.Repository;
public class ReservationRepository : IReservationRepository
{


	private readonly RestaurantContext _context;
	public ReservationRepository(RestaurantContext context)
	{
		_context = context;
	}



	public void Add(Reservation Entity)
	{
		Entity.Id = 0;
		_context.Set<Reservation>().Add(Entity);
	}

	public bool CheckIfCustomerHasAMealReservation(long customerId, long mealEntryId)
	{
		return _context.Set<Reservation>()
			   .Where(reservation => reservation.CustomerId == customerId)
			   .Where(reservation => reservation.MealEntryId == mealEntryId)
			   .Any(reservation => reservation.ReservationStatus != OrderStatus.Canceled.ToString());
	}

	public void Delete(Reservation Entity)
	{
		throw new NotImplementedException();

	}

	public void DeleteAll(ICollection<Reservation> reservations)
	{
		_context.Set<Reservation>().RemoveRange(reservations);
	}

	public IEnumerable<Reservation> GetAll()
	{
		throw new NotImplementedException();
	}

	public List<Reservation> GetBetweenTwoDate(DateOnly start, DateOnly end)
	{
		DateTime normalizedStart = new DateTime(start.Year, start.Month, start.Day);
		DateTime normalizedEnd = new DateTime(end.Year, end.Month, end.Day);

		return _context.Set<Reservation>()
			.Where(reservation => reservation.AtDay >= normalizedStart && reservation.AtDay <= normalizedEnd)
			.ToList();
	}

	public List<Reservation> GetByCustomer(long id)
	{
		return _context.Set<Reservation>()
			.Where(reservation => reservation.CustomerId == id)
			.ToList();
	}

	public List<Reservation> GetByDate(DateOnly day)
	{
		DateTime normalizedDay = new DateTime(day.Year, day.Month, day.Day);

		return _context.Set<Reservation>()
			.Where(reservation => reservation.AtDay == normalizedDay)
			.ToList();
	}

	public Reservation? GetById(long id)
	{
		return _context.Set<Reservation>().Find(id);
	}

	public Reservation? GetFirstOnWaitingToCancelWhereHisBalanceIsEnough(long mealEntryId)
	{
		return _context.Set<Reservation>()
		.Where(reservation => reservation.MealEntryId == mealEntryId)
		.Where(reservation => reservation.ReservationStatus ==
				OrderStatus.OnTheCanceledListButNotCanceledYet.ToString())
		.Include(reservation => reservation.Customer)
		.Where(reservation => reservation.Customer.Balance > reservation.Price)
		.OrderBy(reservation => reservation.NumberInQueue)
		.FirstOrDefault();
	}

	public Reservation? GetFirstWaitingReservationOnEntry(long entryId)
	{
		return _context.Set<Reservation>()
							.Where(reservation => reservation.MealEntryId == entryId)
							.Where(reservation => reservation.ReservationStatus == OrderStatus.Waiting.ToString())
							.Include(reservation => reservation.Customer)
							.OrderBy(reservation => reservation.NumberInQueue)
							.Where(reservation => reservation.Customer.Balance >= reservation.Price)
							.FirstOrDefault();
	}


	public List<Reservation> GetOnWaitingToCancel(long mealEntryId)
	{
		return _context.Set<Reservation>()
		.Where(reservation => reservation.MealEntryId == mealEntryId)
		.Where(reservation => reservation.ReservationStatus ==
				OrderStatus.OnTheCanceledListButNotCanceledYet.ToString())
		.Include(reservation => reservation.Customer)
		.OrderBy(reservation => reservation.NumberInQueue)
		.ToList();
	}

	public IEnumerable<Reservation> GetPage(int pageSize, int pageNumber)
	{
		throw new NotImplementedException();
	}

	public Reservation? GetReservationOnMealEntryBySerialNumber(long mealEntryId, int serialNumber)
	{
		return _context.Set<Reservation>()
			.Where(reservation => reservation.MealEntryId == mealEntryId)
			.Include(reservation => reservation.Customer)
			.Include(reservation => reservation.MealEntry)
			.Where(reservation => reservation.Customer.SerialNumber == serialNumber)
			.FirstOrDefault();
	}

	public List<ReservationsCustomerTypeReadModel> GetReservationsGroupedByCustomersTypeOnMeal(long mealEntryId)
	{
		var answer = _context.Set<Reservation>()
			.Where(reservation => reservation.MealEntryId == mealEntryId)
			.Include(reservation => reservation.Customer)
			.AsEnumerable()
			.GroupBy(reservation => reservation.Customer.Category)
			.Select(group => new ReservationsCustomerTypeReadModel()
			{
				CustomerType = group.Key,
				NumberOfCustomers = group.Count()
			})
			.ToList();

		return answer;
	}

	public List<Reservation> GetYesterdayReservationsThatPassed()
	{
		return _context.Set<Reservation>()
			.Where(reservation => reservation.AtDay >= DateTime.Now.AddDays(-1))
			.Where(reservation => reservation.AtDay < DateTime.Now)
			.Where(reservation => reservation.ReservationStatus == OrderStatus.Reserved.ToString()
			|| reservation.ReservationStatus == OrderStatus.OnTheCanceledListButNotCanceledYet.ToString())
			.ToList();
	}

	//public Reservation? GetTheFirstOfWaitingReservationsOnEntry(long id)
	//{
	//	return _context.Set<Reservation>()
	//		.Where(reservation=> reservation.MealEntryId == id)
	//		.Where(reservation => reservation.ReservationStatus == OrderStatus.Waiting.ToString())
	//		.OrderBy(reservation => reservation.NumberInQueue)
	//		.FirstOrDefault();
	//}

	public List<Reservation> GetTheKFirstWaitingReservationsOnEntry(long mealEntryId, int K)
	{
		return _context.Set<Reservation>()
		.Where(reservation => reservation.MealEntryId == mealEntryId)
		.Where(reservation => reservation.ReservationStatus == OrderStatus.Waiting.ToString())
		.Include(reservation => reservation.Customer)
		.OrderBy(reservation => reservation.NumberInQueue)
		.Take(K)
		.ToList();
	}

	public void Update(Reservation Entity)
	{
		_context.Set<Reservation>().Update(Entity);
	}

	public void UpdateAll(List<Reservation> reservationsToPass)
	{
		_context.Set<Reservation>().UpdateRange(reservationsToPass);
	}

	public ReservationsReadModel GetReservationsOnMeal(long mealEntryId)
	{
		List<Reservation> reservations = _context.Set<Reservation>()
			.AsNoTracking()
			.Where(reservation => reservation.MealEntryId == mealEntryId)
			.Include(reservation => reservation.MealEntry)
			.ThenInclude(meal => meal.MealInformation)
			.Include(reservation => reservation.Customer)
			.OrderBy(reservation => reservation.Id)
			.ToList();

		MealEntry? meal = _context.Set<MealEntry>()
			.AsNoTracking()
			.Include(mealEntry => mealEntry.MealInformation)
			.FirstOrDefault(e => e.Id == mealEntryId);

		if (meal is null)
		{
			throw new Exception("MealEntry? meal = _context.Set<MealEntry>().FirstOrDefault(e => e.Id == mealEntryId);");
		}

		return new ReservationsReadModel()
		{
			Records = reservations.Select(reservation =>

				new ReservationRecord()
				{
					Id = reservation.Id,
					CustomerCategory = reservation.Customer.Category,
					CustomerName = reservation.Customer.FirstName + " " + reservation.Customer.LastName,
					SerialNumber = reservation.Customer.SerialNumber,
					Status = reservation.ReservationStatus
				}
			).ToList(),
			MealReadModel = new MealEntryReadModel()
			{
				Id = meal.Id,
				Name = meal.MealInformation.Name,
				ConsumedCount = meal.ConsumedReservations,
				ReservationsCount = meal.ReservationsCount,
				Day=meal.AtDay.ToString("dd/MM/yyyy")
			},

		};
	}
}
