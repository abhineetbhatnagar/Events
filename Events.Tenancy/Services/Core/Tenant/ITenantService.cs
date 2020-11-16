using Events.Tenancy.Services.Domain;


namespace Events.Tenancy.Services.Core.Tenant{
    public interface ITenantService{
        AuthServiceResponse AuthenticateUser(LoginModel InputModel);
    }
}