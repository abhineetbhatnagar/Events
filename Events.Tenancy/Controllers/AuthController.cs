using Events.Tenancy.Services.Core.Tenant;
using Events.Tenancy.Services.Domain.Entities;
using Events.Tenancy.Services.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events.Tenancy.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        public AuthController(ITenantService tenantService){
            this._tenantService = tenantService;
        }

        [HttpPost]
        [Route("authenticate-user")]
        public IActionResult AuthenticateUser([FromBody]LoginModel InputData){
            AuthServiceResponse authResponse = _tenantService.AuthenticateUser(InputData);
            if(authResponse.Status){
                return Ok(_tenantService.AuthenticateUser(InputData).Auth_Token);    
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
