using Domain.Customers.Aggregate;
using Domain.Customers.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.UseCases.Customers.GetByFilter;
internal class GetCustomerBySerialNumberQueryHandler : IQueryHandler<GetCustomerBySerialNumberQuery, Customer>
{
	private ICustomerRepository _customerRepository;

	private IUnitOfWork _unitOfWork;

	public GetCustomerBySerialNumberQueryHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
	{
		_customerRepository = customerRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Customer>> Handle(GetCustomerBySerialNumberQuery request, CancellationToken cancellationToken)
	{
		try
		{
			Customer? response = _customerRepository.GetBySerialNumber(request.serialNumber);
			await _unitOfWork.SaveChangesAsync();

			if (response is null)
			{
				return Result.Failure<Customer>(new SharedKernal.Utilities.Errors.Error("خطأ", "مع الأسف لا يوجد أي شخص يحمل الرقم الذاتي المطلوب تأكد من طلبك وحاول ثانيةً."));
			}

			return Result.Success(response);
		}
		catch (Exception exception)
		{
			return Result.Failure<Customer>(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
