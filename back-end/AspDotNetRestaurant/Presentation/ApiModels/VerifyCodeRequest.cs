namespace Presentation.ApiModels;

public class VerifyCodeRequest
{
	public long EntryId { get; set; }
	public string Code { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public int SerialNumber { get; set; }
}
