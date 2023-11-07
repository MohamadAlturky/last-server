using Infrastructure.Authentication.Models;

namespace Infrastructure.ForgetPasswordHandling.Models;

public class ForgetPasswordEntry
{
	public long Id { get; set; }
	public int SerialNumber { get; set; }
	public long UserId { get; set; }
	public string Email { get; set; } = string.Empty;
	public string ValidationToken { get; set; } = string.Empty;
	public User User { get; set; }
	public DateTime AtDay { get; set; }
}
