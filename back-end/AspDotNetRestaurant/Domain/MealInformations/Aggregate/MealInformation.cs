using Domain.MealEntries.Aggregate;
using Domain.Meals.ValueObjects;
using SharedKernal.Entities;

namespace Domain.MealInformations.Aggregate;
public class MealInformation : AggregateRoot
{
	private MealType _type = MealType.UnDefined;
	private NumberOfCalories _numberOfCalories = new NumberOfCalories(0);
	private MealDescription _description = new MealDescription("");


	public int NumberOfCalories { get => _numberOfCalories.Value; set => _numberOfCalories = new NumberOfCalories(value); }
	public string Type { get => _type.ToString(); set => _type = Enum.Parse<MealType>(value); }
	public string Description { get => _description.Value; set => _description = new MealDescription(value); }
	public ICollection<MealEntry> MealEntries { get; set; } = new HashSet<MealEntry>();



	public string ImagePath { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;



	public MealInformation(long id) : base(id) { }
	public MealInformation() : base(0) { }
}
