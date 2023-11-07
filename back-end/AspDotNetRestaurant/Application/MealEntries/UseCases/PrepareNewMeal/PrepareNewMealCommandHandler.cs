using Domain.MealEntries.Factories;
using Domain.MealInformations.Aggregate;
using Domain.MealInformations.Repositories;
using Domain.Meals.Repositories;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.Meals.UseCases.PrepareNewMeal;
internal class PrepareNewMealCommandHandler : ICommandHandler<PrepareNewMealCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMealInformationRepository _mealInformationRepository;
	private readonly IMealEntryRepository _mealRepository;

	public PrepareNewMealCommandHandler(IMealEntryRepository mealRepository, 
		IUnitOfWork unitOfWork, 
		IMealInformationRepository mealInformationRepository)
	{
		_mealRepository = mealRepository;
		_unitOfWork = unitOfWork;
		_mealInformationRepository = mealInformationRepository;
	}

	public async Task<Result> Handle(PrepareNewMealCommand request, CancellationToken cancellationToken)
	{
		try
		{
			MealInformation? meal = _mealInformationRepository.GetInformationById(request.mealId);

			if(meal is null)
			{
				return Result.Failure(new Error("", "if(meal is null)"));
			}
			
			DateTime date = new DateTime(request.atDay.Year, 
				request.atDay.Month,
				request.atDay.Day);


			bool checkIfMealHasEntryInDay = _mealInformationRepository.CheckIfMealHasEntryInDay(request.mealId, request.atDay);


			var newMeal = MealFactory.Create(meal.Id, date, request.numberOfUnits, checkIfMealHasEntryInDay);
			//meal.PrepareNewEntry(date, request.numberOfUnits, checkIfMealHasEntryInDay);

			//_mealInformationRepository.UpdateInformation(meal);
			_mealRepository.Add(newMeal);
			await _unitOfWork.SaveChangesAsync();
		}
		catch (Exception exception)
		{
			return Result.Failure(new Error("", exception.Message));
		}

		return Result.Success();
	}
}