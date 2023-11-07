using Infrastructure.Notification.Model;

namespace Infrastructure.Notification.Models;
public class NotificationsPage
{
	public int Count { get; set; }
	public List<NotificationMessage> NotificationMessages { get; set; } = new();
}
