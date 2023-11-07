namespace Infrastructure.DataAccess.UserPersistence.Models;

public class CustomerInformation
{
	public long Id { get; set; }
	public string HiastMail { get; set; } = string.Empty;
	public int SerialNumber { get; set; }
	public int Balance { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string Category { get; set; } = string.Empty;
	public string Notes { get; set; } = string.Empty;
	public string BelongsToDepartment { get; set; } = string.Empty;
}
