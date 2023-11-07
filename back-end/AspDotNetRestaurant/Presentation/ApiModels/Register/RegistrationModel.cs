using System.ComponentModel.DataAnnotations;

namespace Presentation.ApiModels.Register;

public class RegistrationModel
{
	//public List<int> Roles { get; set; }

	[Required]
	public int SerialNumber { get; set; }

	[Required]
	public string FirstName { get; set; } = string.Empty;

	[Required]
	public string LastName { get; set; } = string.Empty;

	[Required]
	public string Category { get; set; } = string.Empty;


	public string BelongsToDepartment { get; set; } = "UNDefined";


	public string Notes { get; set; } = string.Empty;

	public bool IsRegular { get; set; } = true;
	public bool IsActive { get; set; } = true;

	public bool Eligible { get; set; } = true;


	[Required]
	public string Password { get; set; } = string.Empty;

	public string HiastMail { get; set; } = string.Empty;
}
