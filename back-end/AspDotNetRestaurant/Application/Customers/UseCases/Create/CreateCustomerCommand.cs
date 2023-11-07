using Domain.Customers.Aggregate;
using SharedKernal.CQRS.Commands;

namespace Application.UseCases.Customers.Create;

public record CreateCustomerCommand(Customer customer) : ICommand;
