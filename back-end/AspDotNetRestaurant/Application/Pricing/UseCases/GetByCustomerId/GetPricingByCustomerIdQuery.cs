using Domain.Pricing.Aggregate;
using SharedKernal.CQRS.Queries;

namespace Application.Pricing.UseCases.GetByCustomerId;
public sealed record GetPricingByCustomerIdQuery(long customerId) : IQuery<List<PricingRecord>>;
