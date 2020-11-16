using Events.Manager.Services.Core;
using Events.Manager.Services.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// API to add participants to an event
        /// </summary>
        /// <param name="InputModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add-participants")]
        public async Task<IActionResult> AddParticipants([FromBody] Participants InputModel) {
            await _participantsService.AddParticipants(InputModel);
            return Ok();
        }
        
        /// <summary>
        /// API to fetch all participants for an event
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("fetch-event-participants/{id}")]
        public IActionResult FetchEventParticipants(string id)
        {
            return Ok(_participantsService.FetchParticipantsForEvent(id));
        }
    }
}
