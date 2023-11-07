using Application.UseCases.Customers.IncreaseCustomerBalance;
using Domain.Customers.Aggregate;
using Domain.Customers.Exceptions;
using Domain.Customers.Repositories;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;
using System;

namespace Application.Customers.UseCases.DecreaseCustomerBalance;
internal class DecreaseCustomerBalanceCommandHandler : ICommandHandler<DecreaseCustomerBalanceCommand>
{

	private readonly ICustomerRepository _customerRepository;

	private readonly IUnitOfWork _unitOfWork;

	public DecreaseCustomerBalanceCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
	{
		_customerRepository = customerRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(DecreaseCustomerBalanceCommand request, CancellationToken cancellationToken)
	{
		if (request.valueToRemoveFromTheBalance <= 0)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", ""));
		}
		try
		{
			Customer? customer = _customerRepository.GetBySerialNumber(request.serialNumber);

			if (customer is null)
			{
				#warning needs refactoring
				throw new Exception();
			}

			customer.DecreaseBalance(request.valueToRemoveFromTheBalance);

			_customerRepository.Update(customer);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success();
		}
		catch (NegativeBalanceException exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));

			//return Result.Failure(CustomerErrorsDictionary.DataBaseIgnoredToIncreaseTheBalance("لا يوجد رصيد كافي لدى الزبون صاحب الرقم الذاتي "+" - "+ request.serialNumber));
		}
		catch (Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
