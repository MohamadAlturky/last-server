using Domain.MealInformations.Aggregate;
using Domain.Meals.ValueObjects;
using SharedKernal.CQRS.Queries;

namespace Application.Meals.UseCases.GetMealsByName;
public record GetMealsByNameQuery(string mealName,MealType type):IQuery<List<MealInformation>>;