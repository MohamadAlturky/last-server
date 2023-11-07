using Domain.Customers.ReadModels;
using Domain.Customers.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.Customers.UseCases.GetAccountTransactions;
internal sealed class GetAccountTransactionQueryHandler 
	: IQueryHandler<GetAccountTransactionsQuery, AccountTransactionsReadModel>
{

	private readonly ICustomerRepository _customerRepository;
	private readonly IUnitOfWork _unitOfWork;

	public GetAccountTransactionQueryHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
	{
		_customerRepository = customerRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<AccountTransactionsReadModel>> Handle(GetAccountTransactionsQuery request,
		CancellationToken cancellationToken)
	{
		try
		{
			AccountTransactionsReadModel? accountTransactionsReadModel
				= _customerRepository.GetAccountTransactionsPage(request.serialNumber, request.pageNumber);
			if (accountTransactionsReadModel == null)
			{
				return Result.Failure<AccountTransactionsReadModel>(new Error("", "if (accountTransactionsReadModel == null)"));
			}
			await _unitOfWork.SaveChangesAsync();
			return Result.Success(accountTransactionsReadModel);
		}
		catch (Exception exception)
		{
			return Result.Failure<AccountTransactionsReadModel>(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
