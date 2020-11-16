using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Tenancy.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Route("test")]
        public IActionResult GetData()
        {
            return Ok("Tenancy working fine.");
        }
    }
}
