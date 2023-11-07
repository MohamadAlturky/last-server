using Domain.Customers.Aggregate;
using SharedKernal.CQRS.Queries;

namespace Application.UseCases.Customers.GetPage;
public sealed record GetCustomersPageQuery(int pageSize, int pageNumber)
	: IQuery<List<Customer>>;
