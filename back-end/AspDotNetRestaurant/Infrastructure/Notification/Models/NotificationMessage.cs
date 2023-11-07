namespace Infrastructure.Notification.Model;
public class NotificationMessage
{
	public long Id { get; set; }
	public string MessageSubject { get; set; } = string.Empty;
	public string MessageContent { get; set; } = string.Empty;
	public DateTime? SentAt { get; set; }
}
