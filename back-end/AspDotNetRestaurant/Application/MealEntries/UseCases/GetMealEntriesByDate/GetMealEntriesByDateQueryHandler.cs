using Domain.MealEntries.Aggregate;
using Domain.Meals.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Meals.UseCases.GetMealEntriesByDate;
internal class GetMealEntriesByDateQueryHandler : IQueryHandler<GetMealEntriesByDateQuery, List<MealEntry>>
{
	private readonly IUnitOfWork _unitOfWork;

	private readonly IMealEntryRepository _mealRepository;

	public GetMealEntriesByDateQueryHandler(IUnitOfWork unitOfWork, IMealEntryRepository mealRepository)
	{
		_unitOfWork = unitOfWork;
		_mealRepository = mealRepository;
	}

	public async Task<Result<List<MealEntry>>> Handle(GetMealEntriesByDateQuery request, CancellationToken cancellationToken)
	{
		try
		{
			List<MealEntry> response = _mealRepository.GetEntriesByDate(request.date);
			await _unitOfWork.SaveChangesAsync();
			return Result.Success(response);

		}
		catch (Exception exception)
		{
			return Result.Failure<List<MealEntry>>(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
