namespace Events.Manager.Services.Infra.DB.Config  
{  
    public interface IDatabaseConfig  
    {  
        string EventsCollectionName { get; set; }
        string ParticipantsCollectionName { get; set; }
        string ConnectionString { get; set; }  
        string DatabaseName { get; set; }  
    }  
}