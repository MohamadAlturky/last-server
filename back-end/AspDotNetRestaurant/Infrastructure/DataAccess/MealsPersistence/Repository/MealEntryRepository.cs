using Domain.Customers.Aggregate;
using Domain.MealEntries.Aggregate;
using Domain.MealInformations.Aggregate;
using Domain.Meals.Repositories;
using Domain.Meals.ValueObjects;
using Domain.Reservations.Aggregate;
using Domain.Shared.ReadModels;
using Infrastructure.DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Reflection.Metadata;

namespace Infrastructure.MealsPersistence.Repository;

public class MealEntryRepository : IMealEntryRepository
{
	private readonly RestaurantContext _context;
	public MealEntryRepository(RestaurantContext context)
	{
		_context = context;
	}

	public List<MealEntry> GetEntriesByDate(DateOnly date)
	{
		DateTime dateTime = new DateTime(date.Year, date.Month, date.Day);
		return _context.Set<MealEntry>()
			.Where(entry => entry.AtDay == dateTime)
			.Include(entry => entry.MealInformation)
			.AsNoTracking()
			.ToList();
	}


	public IEnumerable<MealEntry> GetMealEntries(long mealId)
	{
		return _context.Set<MealEntry>()
			.Where(mealEntry => mealEntry.MealInformationId == mealId)
			.AsNoTracking()
			.ToList();
	}

	public MealEntry? GetMealEntryById(long entryId)
	{
		return _context.Set<MealEntry>()
			.Include(entry => entry.MealInformation)
			.Where(entry => entry.Id == entryId).FirstOrDefault();
	}

	public List<AutoCompleteModel> GetAutoComplete(string partOfMealName, MealType mealType)
	{
		var type = mealType.ToString();
		return _context.Set<MealInformation>()
			.Where(meal => meal.Type == type)
			.Where(meal => meal.Name.Contains(partOfMealName))
			.Select(meal => new AutoCompleteModel(meal.Id, meal.Name))
			.ToList();
	}

	public List<MealInformation> GetEntriesByNameAndType(string mealName, MealType type)
	{
		var stringType = type.ToString();
		return _context.Set<MealInformation>()
			.Where(meal => meal.Type == stringType)
			.Where(meal => meal.Name.Contains(mealName))
			.ToList();
	}

	public WeeklyPreparedMeals GetWeeklyMealsStartsFrom(DateTime startOfTheWeek, long customerId)
	{
		DateTime endOfTheWeek = startOfTheWeek.AddDays(6);
		WeeklyPreparedMeals response = new WeeklyPreparedMeals();

		response.StartDay = new DateOnly(startOfTheWeek.Year,
										 startOfTheWeek.Month,
										 startOfTheWeek.Day);

		var meals = _context.Set<MealEntry>()
			.OrderBy(entry => entry.AtDay)
			.AsSplitQuery()
			.Where(entry => entry.AtDay >= startOfTheWeek && entry.AtDay <= endOfTheWeek)
			.Include(entry => entry.MealInformation)
			.Include(entry => entry.Reservations
				.Where(res => res.CustomerId == customerId))
			.AsNoTracking()
			.ToList();

		foreach (var entry in meals)
		{
			if (entry.MealInformation is null)
			{
				throw new Exception("if (entry.Meal == null)");
			}
			// error when try to parse to json
		}

		for (int index = 0; index <= 6; index++)
		{
			DailyMeals daily = new DailyMeals();
			daily.AtDay = new DateOnly(startOfTheWeek.AddDays(index).Year,
									   startOfTheWeek.AddDays(index).Month,
									   startOfTheWeek.AddDays(index).Day);

			var dailyMeals = meals.Where(entry =>
				entry.AtDay == startOfTheWeek.AddDays(index))
				.Select(entry => ReadModelsMapper.Map(entry)).ToList();

			foreach (var meal in dailyMeals)
			{
				daily.Meals.Add(meal);
			}

			response.Dailies.Add(daily);
		}

		return response;
	}
	//public WeeklyPreparedMeals GetWeeklyMealsStartsFrom(DateTime startOfTheWeek, long customerId)
	//{
	//	DateTime endOfTheWeek = startOfTheWeek.AddDays(6);

	//	WeeklyPreparedMeals response = new WeeklyPreparedMeals();

	//	response.StartDay = new DateOnly(startOfTheWeek.Year, startOfTheWeek.Month, startOfTheWeek.Day);

	//	//List<MealEntry> meals = _context.Set<MealEntry>()
	//	//					.Where(entry => entry.AtDay >= startOfTheWeek && entry.AtDay <= endOfTheWeek)
	//	//					.Include(entry => entry.Reservations.Where(res => res.CustomerId == customerId))
	//	//					//.Include(entry => entry.MealInformation)
	//	//					.OrderBy(entry => entry.AtDay)
	//	//					.AsNoTracking()
	//	//					.ToList();



	//	//foreach (var meal in meals)
	//	//{
	//	//	meal.MealInformation = _context.Set<MealInformation>().Find(meal.MealInformationId);
	//	//}
	//	var meals = from mealEntry in _context.Set<MealEntry>()
	//				join mealInfo in _context.Set<MealInformation>() on mealEntry.MealInformationId equals mealInfo.Id
	//				join reservation in _context.Set<Reservation>() on mealEntry.Id equals reservation.MealEntryId
	//				where reservation.CustomerId == customerId
	//					&& mealEntry.AtDay >= startOfTheWeek
	//					&& mealEntry.AtDay <= endOfTheWeek
	//				orderby mealEntry.AtDay
	//				select new MealEntry()
	//				{
	//					Id = mealEntry.Id,
	//					MealInformation = mealInfo,
	//					AtDay = mealEntry.AtDay,
	//					ConsumedReservations = mealEntry.ConsumedReservations,
	//					CreatedAt = mealEntry.CreatedAt,
	//					CustomerCanCancel = mealEntry.CustomerCanCancel,
	//					LastNumberInQueue = mealEntry.LastNumberInQueue,
	//					MealInformationId = mealEntry.MealInformationId,
	//					UpdatedAt = mealEntry.UpdatedAt,
	//					PreparedCount = mealEntry.PreparedCount,
	//					ReservationsCount = mealEntry.ReservationsCount,
	//					RowVersion = mealEntry.RowVersion,
	//					Reservations = new List<Reservation>()
	//					{
	//						reservation
	//					}
	//				};




	//	for (int index = 0; index <= 6; index++)
	//	{
	//		DateTime currentDay = startOfTheWeek.AddDays(index);
	//		var dailyMeals = meals.Where(entry => entry.AtDay == currentDay)
	//							  .Select(entry => ReadModelsMapper.Map(entry))
	//							  .ToList();

	//		DailyMeals daily = new DailyMeals()
	//		{
	//			AtDay = new DateOnly(currentDay.Year, currentDay.Month, currentDay.Day),
	//			Meals = dailyMeals
	//		};

	//		response.Dailies.Add(daily);
	//	}

	//	return response;
	//}

	public void Add(MealEntry newMeal)
	{
		newMeal.Id = 0;
		_context.Set<MealEntry>().Add(newMeal);
	}

	public MealEntry? GetMealEntryWithAllInformationAboutReservationsAndCustomers(long mealEntryId)
	{
		return _context.Set<MealEntry>()
			.Include(mealEntry => mealEntry.Reservations)
			.ThenInclude(reservatoin => reservatoin.Customer)
			.FirstOrDefault(mealEntry => mealEntry.Id == mealEntryId);
	}

	public void Delete(MealEntry fullMealEntry)
	{
		_context.Set<MealEntry>().Remove(fullMealEntry);
	}

	public WeeklyPreparedMeals GetMealsScheduleStartsFrom(DateTime startOfTheWeek)
	{
		DateTime endOfTheWeek = startOfTheWeek.AddDays(6);
		WeeklyPreparedMeals response = new WeeklyPreparedMeals();

		response.StartDay = new DateOnly(startOfTheWeek.Year,
										 startOfTheWeek.Month,
										 startOfTheWeek.Day);

		var meals = _context.Set<MealEntry>()
			.AsSplitQuery()
			.Where(entry => entry.AtDay >= startOfTheWeek)
			.Where(entry => entry.AtDay <= endOfTheWeek)
			.Include(entry => entry.MealInformation)
			.OrderBy(entry => entry.AtDay)
			.AsNoTracking()
			.ToList();

		foreach (var entry in meals)
		{
			if (entry.MealInformation is null)
			{
				throw new Exception("if (entry.Meal == null)");
			}
			// error when try to parse to json
		}

		for (int index = 0; index <= 6; index++)
		{
			DailyMeals daily = new DailyMeals();
			daily.AtDay = new DateOnly(startOfTheWeek.AddDays(index).Year,
									   startOfTheWeek.AddDays(index).Month,
									   startOfTheWeek.AddDays(index).Day);

			var dailyMeals = meals.Where(entry =>
				entry.AtDay == startOfTheWeek.AddDays(index))
				.Select(entry => ReadModelsMapper.Map(entry)).ToList();

			foreach (var meal in dailyMeals)
			{
				daily.Meals.Add(meal);
			}

			response.Dailies.Add(daily);
		}

		return response;
	}
}
