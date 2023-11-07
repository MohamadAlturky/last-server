using Domain.Customers.Aggregate;
using Domain.Customers.Repositories;
using Domain.Reservations.Aggregate;
using Domain.Reservations.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.Reservations.UseCases.GetByCustomerSerialNumber;
internal class GetReservationsByCustomerSerialNumberQueryHandler : IQueryHandler<GetReservationsByCustomerSerialNumberQuery, List<Reservation>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IReservationRepository _reservationRepository;
	private readonly ICustomerRepository _customerRepository;

	public GetReservationsByCustomerSerialNumberQueryHandler(IUnitOfWork unitOfWork, IReservationRepository reservationRepository, ICustomerRepository customerRepository)
	{
		_unitOfWork = unitOfWork;
		_reservationRepository = reservationRepository;
		_customerRepository = customerRepository;
	}

	public async Task<Result<List<Reservation>>> Handle(GetReservationsByCustomerSerialNumberQuery request, CancellationToken cancellationToken)
	{
		try
		{
			Customer? customer = _customerRepository.GetBySerialNumber(request.serialNumber);
			
			if(customer is null)
			{
				throw new Exception("Customer? customer = _customerRepository.GetBySerialNumber(request.serialNumber);");
			}

			List<Reservation> reservations = _reservationRepository.GetByCustomer(customer.Id);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success(reservations);

		}
		catch (Exception exception)
		{
			return Result.Failure<List<Reservation>>(new Error("", exception.Message));
		}
	}
}
