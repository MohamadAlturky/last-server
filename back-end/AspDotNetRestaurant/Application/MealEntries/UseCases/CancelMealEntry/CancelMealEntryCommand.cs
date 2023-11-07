using SharedKernal.CQRS.LogableCommand;
using SharedKernal.Utilities.Result;

namespace Application.MealEntries.UseCases.CancelMealEntry;
public record CancelMealEntryCommand(long mealEntryId) : ILogableCommand<Result>;
