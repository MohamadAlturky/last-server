using Infrastructure.Notification.Model;
using Infrastructure.Notification.Models;

namespace Infrastructure.Notification.Services;

public interface INotificationService
{
	Task SendToAllAsync(NotificationMessage notificationMessage);
	Task<NotificationsPage> GetNotificationPage(int pageNumber);
}
