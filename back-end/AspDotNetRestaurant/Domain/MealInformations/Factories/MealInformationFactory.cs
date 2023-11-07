using Domain.MealInformations.Aggregate;

namespace Domain.MealInformations.Factories;
public static class MealInformationFactory
{
	public static MealInformation Create(int numberOfCalories,
		string type,
		string description,
		string imagePath,
		string name,
		long id = 0)
	{

		return new MealInformation(id)
		{
			NumberOfCalories = numberOfCalories,
			Type = type,
			Description = description,
			ImagePath = imagePath,
			Name = name,
		};
	}
}
