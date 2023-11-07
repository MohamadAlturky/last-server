using Domain.Pricing.Aggregate;
using SharedKernal.CQRS.Commands;

namespace Application.Pricing.UseCases.Create;
public sealed record CreatePricingRecordCommand(PricingRecord record) : ICommand;