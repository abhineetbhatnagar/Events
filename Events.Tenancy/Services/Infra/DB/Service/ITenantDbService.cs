using Events.Tenancy.Services.Domain.Entities;
namespace Events.Tenancy.Services.Infra.DB.Service
{
    public interface ITenantDbService
    {
        /// <summary>
        /// Method to fetch user data from db
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        TenantModel Get(string username);
    }
}