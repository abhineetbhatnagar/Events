using System.Collections.Generic;
using System.Security.Claims;

namespace Events.Tenancy.Services.Infra.JWT{
    public interface IJwtService{

        /// <summary>
        /// Method to generate JWT Token
        /// </summary>
        /// <param name="_Claims"></param>
        /// <param name="expiryMin"></param>
        /// <returns></returns>
        string GenerateJwtToken(List<Claim> _Claims, double expiryMin = 0);
    }
}