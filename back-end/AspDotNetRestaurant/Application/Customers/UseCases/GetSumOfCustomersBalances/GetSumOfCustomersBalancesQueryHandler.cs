using Domain.Customers.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Customers.UseCases.GetSumOfCustomersBalances;
internal class GetSumOfCustomersBalancesQueryHandler : IQueryHandler<GetSumOfCustomersBalancesQuery, long>
{
	private readonly ICustomerRepository _customerRepository;

	private readonly IUnitOfWork _unitOfWork;

	public GetSumOfCustomersBalancesQueryHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
	{
		_customerRepository = customerRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<long>> Handle(GetSumOfCustomersBalancesQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var balances = _customerRepository.CalculateSumOfBalances();
			await _unitOfWork.SaveChangesAsync();
			return Result.Success(balances);
		}
		catch(Exception exception)
		{
			return Result.Failure<long>(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
