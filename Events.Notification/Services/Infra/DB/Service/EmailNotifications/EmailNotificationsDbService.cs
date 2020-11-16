using Events.Notification.Services.Domain.Entities;
using Events.Notification.Services.Infra.DB.Config;
using MongoDB.Driver;
using System;
using System.Collections.Generic;  
using System.Linq;
using System.Threading.Tasks;

namespace Events.Notification.Services.Infra.DB.Service
{
    public class EmailNotificationsDbService : IEmailNotificationsDbService
    {
        private readonly IMongoCollection<NotificationModel> _emailNotifications;
        public EmailNotificationsDbService(IDatabaseConfig settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _emailNotifications = database.GetCollection<NotificationModel>(settings.EmailNotificationCollectionName);
        }

        // Method To Save Notification In DB
        public async Task AddNotificationToDb(NotificationModel notificationData){
           await _emailNotifications.InsertOneAsync(notificationData);
        }
    }
}