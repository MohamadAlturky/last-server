using Domain.MealInformations.Aggregate;
using Domain.MealInformations.Repositories;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Meals.UseCases.Delete;
internal class DeleteMealInformationCommandHandler : ICommandHandler<DeleteMealInformationCommand>
{
	private IUnitOfWork _unitOfWork { get; set; }
	private IMealInformationRepository _mealInformationRepository { get; set; }

	public DeleteMealInformationCommandHandler(IUnitOfWork unitOfWork, IMealInformationRepository mealRepository)
	{
		_unitOfWork = unitOfWork;
		_mealInformationRepository = mealRepository;
	}

	public async Task<Result> Handle(DeleteMealInformationCommand request, CancellationToken cancellationToken)
	{
		try
		{
			bool canNotDelete = _mealInformationRepository.IsThereAnyEntry(request.mealId);

			if (canNotDelete)
			{
				throw new Exception("bool canNotDelete = _mealInformationRepository.IsThereAnyEntry(request.mealId);");
			}

			
			var meal = _mealInformationRepository.GetInformationById(request.mealId);


			if (meal is null)
			{
				throw new Exception("var meal = _mealInformationRepository.GetInformationById(request.mealId);");
			}
			_mealInformationRepository.DeleteInformation(meal);

			await _unitOfWork.SaveChangesAsync();
		}
		catch (Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
		return Result.Success();
	}
}
