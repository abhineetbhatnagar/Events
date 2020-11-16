namespace Events.Manager.Services.Infra.Messaging.Config{
    public class EventMessagingConfig: IEventMessagingConfig{
        public string Hostname { get; set; }
        public string QueueName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}