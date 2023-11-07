namespace Domain.Shared.Utilities;
public static class Date
{
	public static DateTime ToDay
	{
		get => new DateTime(_toDay.Year, _toDay.Month, _toDay.Day);
	}
	public static DateTime LastSaturday
	{
		get => _getlastSaturday();
	}
	public static DateTime LastSaturdayFrom(DateTime dateTime)
	{
		
		if (dateTime.DayOfWeek == DayOfWeek.Saturday)
		{
			return dateTime;
		}
		else
		{
			DateTime lastSaturday = dateTime.AddDays(-1 * ((int)dateTime.DayOfWeek + 1));
			return lastSaturday;
		}
	}

	private static DateOnly _toDay => new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
	private static DateTime _getlastSaturday()
	{
		DateTime today = DateTime.Now;
		if (today.DayOfWeek == DayOfWeek.Saturday)
		{
			return today;
		}
		else
		{
			DateTime lastSaturday = today.AddDays(-1 * ((int)today.DayOfWeek + 1));
			return lastSaturday;
		}
	}
}
