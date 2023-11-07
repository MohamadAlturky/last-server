using SharedKernal.CQRS.Commands;

namespace Application.Meals.UseCases.PrepareNewMeal;
public sealed record PrepareNewMealCommand(long mealId, DateOnly atDay, 
	int numberOfUnits) : ICommand; 
