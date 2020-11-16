namespace Events.Manager.Services.Infra.DB.Config  
{
    public class DatabaseConfig : IDatabaseConfig
    {
        public string EventsCollectionName { get; set; }
        public string ParticipantsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

}