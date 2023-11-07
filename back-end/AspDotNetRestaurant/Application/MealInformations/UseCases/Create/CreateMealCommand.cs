using Domain.MealInformations.Aggregate;
using SharedKernal.CQRS.Commands;

namespace Application.UseCases.Meals.Create;
public sealed record CreateMealCommand(MealInformation Meal) : ICommand;
