using SharedKernal.CQRS.Commands;

namespace Application.Meals.UseCases.Delete;
public sealed record DeleteMealInformationCommand(long mealId) : ICommand;