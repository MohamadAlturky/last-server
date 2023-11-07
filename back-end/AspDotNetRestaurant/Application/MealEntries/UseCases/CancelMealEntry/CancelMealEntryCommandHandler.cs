using Domain.Customers.Aggregate;
using Domain.Customers.Repositories;
using Domain.MealEntries.Aggregate;
using Domain.MealEntries.Services;
using Domain.Meals.Repositories;
using Domain.Reservations.Repositories;
using SharedKernal.CQRS.LogableCommand;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.MealEntries.UseCases.CancelMealEntry;
internal class CancelMealEntryCommandHandler : LogableCommandHandler<CancelMealEntryCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMealEntryRepository _mealEntryRepository;
	private readonly IReservationRepository _reservationRepository;
	private readonly ICustomerRepository _customerRepository;
	private readonly IMealEntryService _mealEntryService;

	public CancelMealEntryCommandHandler(IUnitOfWork unitOfWork, 
		IMealEntryRepository mealEntryRepository, 
		IReservationRepository reservationRepository, 
		IMealEntryService mealEntryService,
		ICustomerRepository customerRepository
		)
	{
		_unitOfWork = unitOfWork;
		_mealEntryRepository = mealEntryRepository;
		_reservationRepository = reservationRepository;
		_mealEntryService = mealEntryService;
		_customerRepository = customerRepository;
	}

	public async Task<Result> Handle(CancelMealEntryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			MealEntry? fullMealEntry = _mealEntryRepository.
				GetMealEntryWithAllInformationAboutReservationsAndCustomers(request.mealEntryId);
			
			if(fullMealEntry is null)
			{
				throw new Exception("if(fullMealEntry is null)");
			}

			_mealEntryService.ReturnMoneyForCutomers(fullMealEntry);
			
			List<Customer> customers = new List<Customer>();
			foreach(var reservation in fullMealEntry.Reservations)
			{
				if(reservation.Customer is null)
				{
					throw new Exception("reservation.Customer is null");
				}
				customers.Add(reservation.Customer);
			}

			_customerRepository.UpdateAll(customers);
			_reservationRepository.DeleteAll(fullMealEntry.Reservations);
			_mealEntryRepository.Delete(fullMealEntry);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success();
		}
		catch (Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
