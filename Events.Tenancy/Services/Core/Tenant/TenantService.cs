using System.Collections.Generic;
using System.Security.Claims;
using Events.Tenancy.Services.Domain;
using Events.Tenancy.Services.Infra.DB.Service;
using Events.Tenancy.Services.Infra.Encryption.Service;
using Events.Tenancy.Services.Infra.JWT;


namespace Events.Tenancy.Services.Core.Tenant
{
    public class TenantService : ITenantService
    {

        private readonly ITenantDbService _dbService;
        private readonly IJwtService _jwtService;
        private readonly IEncryptionService _encryptionService;
        public TenantService(ITenantDbService dbService, IJwtService jwtService, IEncryptionService encryptionService)
        {
            this._dbService = dbService;
            this._jwtService = jwtService;
            this._encryptionService = encryptionService;
        }
        public AuthServiceResponse AuthenticateUser(LoginModel InputModel)
        {
            TenantModel searchedTenant = _dbService.Get(InputModel.Username);
            if (searchedTenant != null)
            {
                if (searchedTenant.Password == InputModel.Password)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim("tenant_id", _encryptionService.Encrypt(searchedTenant._id))
                    };
                    return new AuthServiceResponse
                    {
                        Status = true,
                        Auth_Token = _jwtService.GenerateJwtToken(claims)
                    };
                }
                else
                {
                    return new AuthServiceResponse
                    {
                        Status = false
                    };
                }
            }
            else
            {
                return new AuthServiceResponse
                {
                    Status = false
                };
            }

        }
    }
}