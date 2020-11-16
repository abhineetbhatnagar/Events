namespace Events.Tenancy.Services.Infra.DB.Config  
{
    public class DatabaseConfig : IDatabaseConfig
    {
        public string TenantCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

}