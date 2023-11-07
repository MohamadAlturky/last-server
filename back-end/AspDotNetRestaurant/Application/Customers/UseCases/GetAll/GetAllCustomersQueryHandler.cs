using Domain.Customers.Aggregate;
using Domain.Customers.Repositories;
using SharedKernal.CQRS.LogableQuery;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Customers.UseCases.GetAll;
internal class GetAllCustomersQueryHandler : ILogableQueryHandler<GetAllCustomersQuery, Result<List<Customer>>>
{

	private readonly ICustomerRepository _customerRepository;

	private readonly IUnitOfWork _unitOfWork;

	public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
	{
		_customerRepository = customerRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<List<Customer>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
	{
		try
		{
			List<Customer> response = _customerRepository.GetAll().ToList();
			await _unitOfWork.SaveChangesAsync();
			return Result.Success(response);
		}
		catch (Exception exception)
		{
			return Result.Failure<List<Customer>>(new SharedKernal.Utilities.Errors.Error("", exception.Message));

		}
	}
}
