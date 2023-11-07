using Domain.MealInformations.Repositories;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.UseCases.Meals.Create;
public class CreateMealCommandHandler : ICommandHandler<CreateMealCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMealInformationRepository _mealRepository;


	public CreateMealCommandHandler(IUnitOfWork unitOfWork, IMealInformationRepository mealRepository)
	{
		_unitOfWork = unitOfWork;
		_mealRepository = mealRepository;
	}


	public async Task<Result> Handle(CreateMealCommand request, CancellationToken cancellationToken)
	{

		try
		{
			_mealRepository.AddInformation(request.Meal);

			await _unitOfWork.SaveChangesAsync();
		}
		catch(Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("",exception.Message));
		}

		return Result.Success();
	}
}

