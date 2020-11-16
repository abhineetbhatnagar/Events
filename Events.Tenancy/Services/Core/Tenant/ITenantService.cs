using Events.Tenancy.Services.Domain.Models;
using Events.Tenancy.Services.Domain.Entities;


namespace Events.Tenancy.Services.Core.Tenant{
    public interface ITenantService{
        AuthServiceResponse AuthenticateUser(LoginModel InputModel);
    }
}