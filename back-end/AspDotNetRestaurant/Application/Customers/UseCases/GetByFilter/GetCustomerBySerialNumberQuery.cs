using Domain.Customers.Aggregate;
using SharedKernal.CQRS.Queries;

namespace Application.UseCases.Customers.GetByFilter;
public sealed record GetCustomerBySerialNumberQuery(int serialNumber) : IQuery<Customer>;
