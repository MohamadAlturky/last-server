using Domain.Shared.ReadModels;
using SharedKernal.CQRS.Queries;

namespace Application.Meals.UseCases.GetMealsSchedule;
public record GetMealsScheduleQuery(DateOnly dayOfTheWeek) : IQuery<WeeklyPreparedMeals>;