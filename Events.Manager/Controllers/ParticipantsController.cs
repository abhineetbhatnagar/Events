using Events.Manager.Services.Core;
using Events.Manager.Services.Domain.Entities;
using Events.Manager.Services.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Manager.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantsService _participantsService;
        public ParticipantsController(IParticipantsService participantsService)
        {
            this._participantsService = participantsService;
        }

        [HttpPost]
        [Route("add-participants")]
        public IActionResult AddParticipants([FromBody] Participants InputModel) {
            _participantsService.AddParticipants(InputModel);
            return Ok();
        }

        [HttpGet]
        [Route("fetch-event-participants/{id}")]
        public IActionResult FetchEventParticipants(string id)
        {
            return Ok(_participantsService.FetchParticipantsForEvent(id));
        }
    }
}
