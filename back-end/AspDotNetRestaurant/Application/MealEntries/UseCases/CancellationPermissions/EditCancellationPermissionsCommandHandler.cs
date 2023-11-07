using Application.Meals.UseCases.CancellationPermissions;
using Domain.MealEntries.Aggregate;
using Domain.MealInformations.Aggregate;
using Domain.MealInformations.Repositories;
using Domain.Meals.Repositories;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.Meals.UseCases.DisableAllowCancellation;
internal class EditCancellationPermissionsCommandHandler
	: ICommandHandler<EditCancellationPermissionsCommand>
{
	private readonly IUnitOfWork _unitOfWork;

	private readonly IMealEntryRepository _mealEntryRepository;

	public EditCancellationPermissionsCommandHandler(IUnitOfWork unitOfWork,
		IMealEntryRepository mealEntryRepository)
	{
		_unitOfWork = unitOfWork;
		_mealEntryRepository = mealEntryRepository;
	}

	public async Task<Result> Handle(EditCancellationPermissionsCommand request,
		CancellationToken cancellationToken)
	{
		try
		{
			MealEntry? entry = _mealEntryRepository.GetMealEntryById(request.preparedMealId);

			if (entry is null)
			{
				throw new Exception("if(entry is null)");
			}

			entry.ModifyCancellationState(request.cancellationState);

			await _unitOfWork.SaveChangesAsync();

		}
		catch (Exception exception)
		{
			return Result.Failure(new Error("", exception.Message));
		}
		return Result.Success();
	}
}
