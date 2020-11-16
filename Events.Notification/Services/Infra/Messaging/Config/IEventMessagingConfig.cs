namespace Events.Notification.Services.Infra.Messaging.Config{
    public interface IEventMessagingConfig{
        string Hostname { get; set; }

        string QueueName { get; set; }

        string UserName { get; set; }

        string Password { get; set; }
    }
}