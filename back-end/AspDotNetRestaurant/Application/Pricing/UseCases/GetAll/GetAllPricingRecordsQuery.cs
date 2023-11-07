using Domain.Pricing.Aggregate;
using SharedKernal.CQRS.Queries;

namespace Application.Pricing.UseCases.GetAll;
public sealed record GetAllPricingRecordsQuery() : IQuery<List<PricingRecord>>;
