using Domain.MealInformations.Aggregate;
using Domain.MealInformations.Repositories;
using Domain.Meals.Repositories;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Meals.UseCases.Update;
internal class UpdateMealCommandHandler : ICommandHandler<UpdateMealCommand>
{
	private readonly IUnitOfWork _unitOfWork;

	private readonly IMealInformationRepository _mealInformationRepository;

	public UpdateMealCommandHandler(IUnitOfWork unitOfWork,
		IMealInformationRepository mealRepository)
	{
		_unitOfWork = unitOfWork;
		_mealInformationRepository = mealRepository;
	}

	public async Task<Result> Handle(UpdateMealCommand request,
		CancellationToken cancellationToken)
	{

		MealInformation? meal = _mealInformationRepository.GetInformationById(request.meal.Id);

		if (meal is null)
		{
			throw new Exception("MealInformation? meal = _mealInformationRepository.GetInformationById(request.meal.Id);");
		}
		meal.Name = request.meal.Name;
		meal.NumberOfCalories = request.meal.NumberOfCalories;

		bool hasAnyEntries = _mealInformationRepository.IsThereAnyEntry(request.meal.Id);

#warning
		if (hasAnyEntries && request.meal.Type != meal.Type)
		{
			throw new Exception("لا يمكن تغيير نوع وجبة تم تحضير دفعة منها");
		}
			meal.Type = request.meal.Type;
		meal.Description = request.meal.Description;

		_mealInformationRepository.UpdateInformation(meal);

		await _unitOfWork.SaveChangesAsync();

		return Result.Success();
	}
}
