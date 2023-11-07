namespace Presentation.ApiModels.User;

public class UserDTO
{
	public long Id { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string HiastMail { get; set; } = string.Empty;
	public string MacAddress { get; set; } = string.Empty;
	public int Balance{ get; set; }
}
