using Domain.Customers.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ApiModels.Customers;

public class CustomerDTO
{
	public long Id { get; set; }

	[Required]
	public int SerialNumber { get; set; }

	public int Balance { get; set; }

	[Required]
	public string FirstName { get; set; } = string.Empty;

	[Required]
	public string LastName { get; set; } = string.Empty;

	[Required]
	public string Category { get; set; } = string.Empty;


	[Required]
	public string BelongsToDepartment { get; set ; } = string.Empty;

	[Required]
	public string Notes { get ; set; } = string.Empty;


	[Required]
	public bool IsRegular { get; set; }
	
	[Required]
	public bool Eligible { get; set; }

	[Required]
    public bool IsActive { get; set; }

	[Required]
	public string Password { get; set; } = string.Empty;

}
