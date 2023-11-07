using SharedKernal.CQRS.Commands;

namespace Application.UseCases.Customers.IncreaseCustomerBalance;
public record IncreaseCustomerBalanceCommand(int serialNumber, int valueToAddToTheBalance) 
	: ICommand;
