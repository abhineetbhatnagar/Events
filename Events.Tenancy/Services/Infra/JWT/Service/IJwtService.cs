using System.Collections.Generic;
using System.Security.Claims;

namespace Events.Tenancy.Services.Infra.JWT{
    public interface IJwtService{
        string GenerateJwtToken(List<Claim> _Claims, double expiryMin = 0);
    }
}