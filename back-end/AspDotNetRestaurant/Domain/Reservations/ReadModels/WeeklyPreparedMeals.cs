namespace Domain.Shared.ReadModels;
public class WeeklyPreparedMeals
{
	public DateOnly StartDay { get; set; }
	public List<DailyMeals> Dailies { get; set; } = new();

}
