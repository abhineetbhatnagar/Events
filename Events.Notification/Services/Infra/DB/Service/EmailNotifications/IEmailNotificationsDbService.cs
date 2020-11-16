using Events.Notification.Services.Domain.Entities;
using System.Threading.Tasks;

namespace Events.Notification.Services.Infra.DB.Service
{
    public interface IEmailNotificationsDbService
    {
        // Method To Save Notification In DB
        Task AddNotificationToDb(NotificationModel notificationData);
    }
}