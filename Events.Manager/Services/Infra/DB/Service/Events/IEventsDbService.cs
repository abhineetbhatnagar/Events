using Events.Manager.Services.Domain.Entities;
using System.Collections.Generic;

namespace Events.Tenancy.Services.Infra.DB.Service
{
    public interface IEventsDbService
    {
        /// <summary>
        /// Method To Create New Event
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        string CreateEvent(Event eventData);

        /// <summary>
        /// Method To Fetch Event By Its ID
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Event GetEventById(string eventId);

        /// <summary>
        /// Method To Fetch List of All Events
        /// </summary>
        /// <returns></returns>
        IEnumerable<Event> GetAllEvents();

        /// <summary>
        /// Method To Fetch List of All Events For a Particular Owner
        /// </summary>
        /// <param name="eventOwner"></param>
        /// <returns></returns>
        IEnumerable<Event> GetEventsForOwner(string eventOwner);

        /// <summary>
        /// Method To Update An Event
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        bool UpdateEvent(Event eventData);

        /// <summary>
        /// Method To Delete An Event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        bool DeleteEvent(string eventId);
    }
}