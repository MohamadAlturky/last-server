using Application.ExecutorProvider;
using Domain.Meals.Repositories;
using Domain.Shared.ReadModels;
using Domain.Shared.Utilities;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Meals.UseCases.GetMealsSchedule;
internal class GetMealsScheduleQueryHandler : IQueryHandler<GetMealsScheduleQuery, WeeklyPreparedMeals>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMealEntryRepository _mealRepository;
	private readonly IExecutorIdentityProvider _identityProvider;

	public GetMealsScheduleQueryHandler(IUnitOfWork unitOfWork, IMealEntryRepository mealRepository, IExecutorIdentityProvider identityProvider)
	{
		_unitOfWork = unitOfWork;
		_mealRepository = mealRepository;
		_identityProvider = identityProvider;
	}

	public async Task<Result<WeeklyPreparedMeals>> Handle(GetMealsScheduleQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var startOfTheWeek
				= Date.LastSaturdayFrom(new
					DateTime(request.dayOfTheWeek.Year,
							 request.dayOfTheWeek.Month,
							 request.dayOfTheWeek.Day));
			string customerId = _identityProvider.GetExecutorSerialNumber();
			WeeklyPreparedMeals response = _mealRepository.GetMealsScheduleStartsFrom(startOfTheWeek);
			await _unitOfWork.SaveChangesAsync();
			return Result.Success(response);

		}
		catch (Exception exception)
		{
			return Result.Failure<WeeklyPreparedMeals>(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}


	}
}
