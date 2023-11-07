using Domain.MealInformations.Aggregate;
using Domain.Meals.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Meals.UseCases.GetMealsByName;
internal class GetMealsByNameQueryHandler : IQueryHandler<GetMealsByNameQuery, List<MealInformation>>
{
	private readonly IUnitOfWork _unitOfWork;

	private readonly IMealEntryRepository _mealRepository;

	public GetMealsByNameQueryHandler(IUnitOfWork unitOfWork, IMealEntryRepository mealRepository)
	{
		_unitOfWork = unitOfWork;
		_mealRepository = mealRepository;
	}

	public async Task<Result<List<MealInformation>>> Handle(GetMealsByNameQuery request, CancellationToken cancellationToken)
	{
		try
		{
			List<MealInformation> response = _mealRepository.GetEntriesByNameAndType(request.mealName, request.type);
			await _unitOfWork.SaveChangesAsync();
			return Result.Success(response);

		}
		catch (Exception exception)
		{
			return Result.Failure<List<MealInformation>>(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
