using Domain.MealInformations.Aggregate;
using Domain.MealInformations.ReadModels;
using Infrastructure.DataAccess.DBContext;
using Infrastructure.Notification.Hubs;
using Infrastructure.Notification.Model;
using Infrastructure.Notification.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel;

namespace Infrastructure.Notification.Services;

public class NotificationService : INotificationService
{
	private readonly static int NOTIFICATION_PAGE_SIZE = int.Parse(Properties.StaticValues.PaginationSize);
	private readonly IHubContext<NotificationsHub> _notificationHubContext;
	private readonly RestaurantContext _context;


	public NotificationService(IHubContext<NotificationsHub> hubContext, RestaurantContext context)
	{
		_notificationHubContext = hubContext;
		_context = context;
	}

	public async Task<NotificationsPage> GetNotificationPage(int pageNumber)
	{
		IOrderedQueryable<NotificationMessage> queryable =
			_context.Set<NotificationMessage>()
			.OrderByDescending(entry => entry.Id);

		int size = queryable.Count();

		List<NotificationMessage> notifications = await queryable
			.Skip(NOTIFICATION_PAGE_SIZE * (pageNumber - 1))
			.Take(NOTIFICATION_PAGE_SIZE)
			.ToListAsync();

		var model = new NotificationsPage()
		{
			Count = size,
			NotificationMessages = notifications
		};
		return model;
	}

	public async Task SendToAllAsync(NotificationMessage notificationMessage)
	{
		_context.Set<NotificationMessage>().Add(notificationMessage);

		await _context.SaveChangesAsync();
		
		await _notificationHubContext.Clients.All.SendAsync("RecieveMessage",
			notificationMessage.MessageSubject,
			notificationMessage.MessageContent,
			notificationMessage.SentAt);
	}
}
