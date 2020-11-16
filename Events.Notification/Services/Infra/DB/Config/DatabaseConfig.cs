namespace Events.Notification.Services.Infra.DB.Config  
{
    public class DatabaseConfig : IDatabaseConfig
    {
        public string EmailNotificationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

}