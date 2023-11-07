using Domain.MealEntries.Aggregate;
using SharedKernal.CQRS.Queries;

namespace Application.Meals.UseCases.GetMealEntries;
public sealed record GetMealEntriesQuery(long mealId): IQuery<List<MealEntry>>;
