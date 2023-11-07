using Domain.Shared.ReadModels;
using SharedKernal.CQRS.Queries;

namespace Application.Meals.UseCases.GetWeeklyMeals;
public record GetWeeklyMealsQuery(DateOnly dayOfTheWeek,long customerId) : IQuery<WeeklyPreparedMeals>;