namespace Events.Tenancy.Services.Infra.DB.Config  
{  
    public interface IDatabaseConfig  
    {  
        string TenantCollectionName { get; set; }  
        string ConnectionString { get; set; }  
        string DatabaseName { get; set; }  
    }  
}