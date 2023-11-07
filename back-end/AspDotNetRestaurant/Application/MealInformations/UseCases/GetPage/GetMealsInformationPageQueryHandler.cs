using Domain.MealInformations.ReadModels;
using Domain.MealInformations.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Application.MealInformations.UseCases.GetPage;
public class GetMealsInformationPageQueryHandler : IQueryHandler<GetMealsInformationPageQuery, MealsInformationReadModel>
{

	private readonly IUnitOfWork _unitOfWork;
	private readonly IMealInformationRepository _mealRepository;

	public GetMealsInformationPageQueryHandler(IUnitOfWork unitOfWork, IMealInformationRepository mealRepository)
	{
		_unitOfWork = unitOfWork;
		_mealRepository = mealRepository;
	}

	public async Task<Result<MealsInformationReadModel>> Handle(GetMealsInformationPageQuery request, CancellationToken cancellationToken)
	{
		try
		{
			MealsInformationReadModel meals = _mealRepository.GetMealsInformationPage(request.pageNumber);


			if (meals == null)
			{
				return Result.Failure<MealsInformationReadModel>(new Error("no data found", ""));
			}

			await _unitOfWork.SaveChangesAsync();

			return Result.Success(meals);
		}
		catch (Exception exception)
		{
			return Result.Failure<MealsInformationReadModel>(new Error("sorry", exception.Message));
		}
	}
}
