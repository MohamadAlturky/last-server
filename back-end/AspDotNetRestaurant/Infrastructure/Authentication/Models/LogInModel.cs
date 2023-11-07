namespace Infrastructure.Authentication.Models;
public class LogInModel
{
	public int SerialNumber { get; set; }
	public string Password { get; set; } = string.Empty;
}
