namespace Events.Notification.Services.Infra.DB.Config  
{  
    public interface IDatabaseConfig  
    {  
        string EmailNotificationCollectionName { get; set; }
        string ConnectionString { get; set; }  
        string DatabaseName { get; set; }  
    }  
}