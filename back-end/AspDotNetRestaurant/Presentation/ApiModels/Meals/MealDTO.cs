using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Presentation.ApiModels.Meals;


public class MealDTO
{
	public long Id { get; set; }

	public string Type { get; set; } = string.Empty;

	public int NumberOfCalories { get; set; }

	public string ImagePath { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	public string Description { get; set; } = string.Empty;
}

