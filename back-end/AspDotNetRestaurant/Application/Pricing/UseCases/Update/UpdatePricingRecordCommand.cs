using SharedKernal.CQRS.Commands;
using SharedKernal.CQRS.LogableCommand;
using SharedKernal.Utilities.Result;

namespace Application.Pricing.UseCases.Update;
public sealed record UpdatePricingRecordCommand(string customerType,string mealType, int price) : ILogableCommand<Result>;