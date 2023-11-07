using Domain.MealInformations.Aggregate;
using Domain.MealInformations.ReadModels;
using SharedKernal.CQRS.Queries;

namespace Application.MealInformations.UseCases.GetPage;
public record GetMealsInformationPageQuery(int pageNumber) 
	: IQuery<MealsInformationReadModel>;