using Domain.Customers.Aggregate;
using Domain.Customers.Exceptions;
using Domain.Customers.Repositories;
using Domain.Localization;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.UseCases.Customers.IncreaseCustomerBalance;
internal sealed class IncreaseCustomerBalanceCommandHandler : ICommandHandler<IncreaseCustomerBalanceCommand>
{
	private readonly ICustomerRepository _customerRepository;
	private readonly IUnitOfWork _unitOfWork;

	public IncreaseCustomerBalanceCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
	{
		_customerRepository = customerRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(IncreaseCustomerBalanceCommand request, CancellationToken cancellationToken)
	{
		if (request.valueToAddToTheBalance <= 0)
		{
			return Result.Failure(new Error("", LocalizationProvider.GetResource(DomainResourcesKeys.NegativeBalanceException)));
		}

		try
		{
			Customer? customer = _customerRepository.GetBySerialNumber(request.serialNumber);

			if (customer is null)
			{
				#warning needs refactoring
				throw new Exception();
			}

			customer.IncreaseBalance(request.valueToAddToTheBalance);

			_customerRepository.AddAccountTransaction(customer.AccountTransactions.First());
			_customerRepository.Update(customer);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success();
		}
		catch (NegativeBalanceException)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", ""));

			//return Result.Failure(CustomerErrorsDictionary.DataBaseIgnoredToIncreaseTheBalance("لا يوجد شخص بلرقم الذاتي المطلوب"));
		}
		catch (Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
