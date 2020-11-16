using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Events.Tenancy.Services.Infra.JWT.Config;
namespace Events.Tenancy.Services.Infra.JWT{

    public class JwtService: IJwtService{

        private readonly IJwtConfig _jwtSettings;
        public JwtService(IJwtConfig jwtSettings){
            this._jwtSettings = jwtSettings;
        }

        /// <summary>
        /// Method to generate JWT Token
        /// </summary>
        /// <param name="_Claims"></param>
        /// <param name="expiryMin"></param>
        /// <returns></returns>
        public string GenerateJwtToken(List<Claim> _Claims, double expiryMin = 0)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            double expiryMinutes = expiryMin != 0 ? Convert.ToDouble(expiryMin) : Convert.ToDouble(_jwtSettings.ExpiryInMinutes);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(_Claims),
                Expires = DateTime.Now.ToUniversalTime().AddMinutes(expiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                IssuedAt = DateTime.UtcNow
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
    
}