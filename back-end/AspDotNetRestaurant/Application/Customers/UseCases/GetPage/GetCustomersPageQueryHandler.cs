using Domain.Customers.Aggregate;
using Domain.Customers.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.UseCases.Customers.GetPage;
internal class GetCustomersPageQueryHandler : IQueryHandler<GetCustomersPageQuery, List<Customer>>
{
	private ICustomerRepository _customerRepository;

	private IUnitOfWork _unitOfWork;

	public GetCustomersPageQueryHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
	{
		_customerRepository = customerRepository;
		_unitOfWork = unitOfWork;
	}


	public async Task<Result<List<Customer>>> Handle(GetCustomersPageQuery request, CancellationToken cancellationToken)
	{

		try
		{
			var page = _customerRepository.GetPage(request.pageSize, request.pageNumber).ToList();


			await _unitOfWork.SaveChangesAsync();

			return Result.Success(page);
		}
		catch (Exception exception)
		{
			return Result.Failure<List<Customer>>(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}

	}
}
