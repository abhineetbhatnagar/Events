using Events.Tenancy.Services.Domain;
namespace Events.Tenancy.Services.Infra.DB.Service
{
    public interface ITenantDbService
    {
        TenantModel Get(string username);
    }
}