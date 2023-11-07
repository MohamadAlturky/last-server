using System.ComponentModel.DataAnnotations;

namespace Presentation.ApiModels;

public class EditCustomerInformationRequest
{

	[Required]
	public int SerialNumber { get; set; }

	[Required]
	public string FirstName { get; set; } = string.Empty;

	[Required]
	public string LastName { get; set; } = string.Empty;

	[Required]
	public string Category { get; set; } = string.Empty;


	public string Notes { get; set; } = string.Empty;

	public string HiastMail { get; set; } = string.Empty;
}
