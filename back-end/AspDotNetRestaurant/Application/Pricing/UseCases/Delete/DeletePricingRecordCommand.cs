using SharedKernal.CQRS.Commands;

namespace Application.Pricing.UseCases.Delete;
public sealed record DeletePricingRecordCommand(long id) : ICommand;