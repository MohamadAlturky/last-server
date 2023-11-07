using SharedKernal.CQRS.Commands;

namespace Application.Customers.UseCases.DecreaseCustomerBalance;
public sealed record DecreaseCustomerBalanceCommand(int serialNumber, int valueToRemoveFromTheBalance) : ICommand;
