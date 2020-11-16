using Events.Manager.Services.Core;
using Events.Manager.Services.Domain.Entities;
using Events.Manager.Services.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Manager.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IEventsService _eventsService;
        public EventsController(IEventsService eventsService) {
            this._eventsService = eventsService;
        }

        [HttpPost]
        [Route("create-event")]
        public IActionResult CreateEvent([FromBody] NewEventModel InputModel)
        {
            // Fetch Tenant ID from Bearer Token
            var tenant_id = @User.Claims.FirstOrDefault(c => c.Type == "tenant_id").Value;
            if (tenant_id == null)
            {
                return Unauthorized();
            }

            // Map values with event model
            Event objEvent = new Event();
            objEvent.Event_Name = InputModel.Event_Name;
            objEvent.Event_Description = InputModel.Event_Description;
            objEvent.Event_Venue = InputModel.Event_Venue;
            objEvent.Event_Owner = tenant_id;
            objEvent.Event_Start_Date = InputModel.Event_Start_Date;
            objEvent.Event_End_Date = InputModel.Event_End_Date;

            // Return successful response to client
            return Ok(_eventsService.CreateEvent(objEvent));
        }

        [HttpGet]
        [Route("fetch-all-events")]
        public IActionResult FetchAllEvents() {
            return Ok(_eventsService.GetAllEvents());
        }

        [HttpGet]
        [Route("fetch-event/{id}")]
        public IActionResult FetchEvent(string id)
        {
            return Ok(_eventsService.GetEventById(id));
        }

        [HttpGet]
        [Route("fetch-owners-event")]
        public IActionResult FetchOwnersEvent()
        {
            // Fetch Tenant ID from Bearer Token
            var tenant_id = @User.Claims.FirstOrDefault(c => c.Type == "tenant_id").Value;
            if (tenant_id == null)
            {
                return Unauthorized();
            }
            return Ok(_eventsService.GetEventsForOwner(tenant_id));
        }

        [HttpPut]
        [Route("update-event")]
        public IActionResult UpdateEvent([FromBody]UpdateEventModel InputModel)
        {
            // Fetch Tenant ID from Bearer Token
            var tenant_id = @User.Claims.FirstOrDefault(c => c.Type == "tenant_id").Value;
            if (tenant_id == null)
            {
                return Unauthorized();
            }
            // Map values with event model
            Event objEvent = new Event();
            objEvent._id = InputModel._id;
            objEvent.Event_Name = InputModel.Event_Name;
            objEvent.Event_Description = InputModel.Event_Description;
            objEvent.Event_Venue = InputModel.Event_Venue;
            objEvent.Event_Owner = tenant_id;
            objEvent.Event_Start_Date = InputModel.Event_Start_Date;
            objEvent.Event_End_Date = InputModel.Event_End_Date;

            return Ok(_eventsService.UpdateEvent(objEvent));
        }

        [HttpDelete]
        [Route("delete-event/{id}")]
        public IActionResult DeleteEvent(string id)
        {
            return Ok(_eventsService.DeleteEvent(id));
        }
    }
}
