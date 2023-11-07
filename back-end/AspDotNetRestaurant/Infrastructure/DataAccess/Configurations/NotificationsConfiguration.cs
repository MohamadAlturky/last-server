using Infrastructure.Notification.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;
internal class NotificationsConfiguration : IEntityTypeConfiguration<NotificationMessage>
{
	public void Configure(EntityTypeBuilder<NotificationMessage> builder)
	{
		
	}
}
