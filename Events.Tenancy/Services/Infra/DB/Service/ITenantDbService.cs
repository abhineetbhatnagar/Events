using Events.Tenancy.Services.Domain.Entities;
namespace Events.Tenancy.Services.Infra.DB.Service
{
    public interface ITenantDbService
    {
        TenantModel Get(string username);
    }
}