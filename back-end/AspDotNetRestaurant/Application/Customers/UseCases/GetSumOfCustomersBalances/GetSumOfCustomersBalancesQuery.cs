using SharedKernal.CQRS.Queries;

namespace Application.Customers.UseCases.GetSumOfCustomersBalances;
public sealed record GetSumOfCustomersBalancesQuery() : IQuery<long>;
