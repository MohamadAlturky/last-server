using Application.ExecutorProvider;
using Domain.Meals.Repositories;
using Domain.Shared.ReadModels;
using Domain.Shared.Utilities;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Meals.UseCases.GetWeeklyMeals;
internal class GetWeeklyMealsQueryHandler : IQueryHandler<GetWeeklyMealsQuery, WeeklyPreparedMeals>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMealEntryRepository _mealRepository;
	private readonly IExecutorIdentityProvider _identityProvider;

	public GetWeeklyMealsQueryHandler(IUnitOfWork unitOfWork, IMealEntryRepository mealRepository, IExecutorIdentityProvider identityProvider)
	{
		_unitOfWork = unitOfWork;
		_mealRepository = mealRepository;
		_identityProvider = identityProvider;
	}

	public Task<Result<WeeklyPreparedMeals>> Handle(GetWeeklyMealsQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var startOfTheWeek
				= Date.LastSaturdayFrom(new
					DateTime(request.dayOfTheWeek.Year,
							 request.dayOfTheWeek.Month,
							 request.dayOfTheWeek.Day));
			string customerId = _identityProvider.GetExecutorSerialNumber();
			WeeklyPreparedMeals response = _mealRepository.GetWeeklyMealsStartsFrom(startOfTheWeek,request.customerId);
			//await _unitOfWork.SaveChangesAsync();
			return Task.FromResult( Result.Success(response));

		}
		catch (Exception exception)
		{
			return Task.FromResult( Result.Failure<WeeklyPreparedMeals>(new SharedKernal.Utilities.Errors.Error("", exception.Message)));
		}


	}
}
