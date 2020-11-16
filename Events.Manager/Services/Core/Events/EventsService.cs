using Events.Manager.Services.Domain.Entities;
using Events.Manager.Services.Infra.Encryption.Service;
using Events.Tenancy.Services.Infra.DB.Service;
using System.Collections.Generic;

namespace Events.Manager.Services.Core
{
    public class EventsService : IEventsService
    {
        private readonly IEventsDbService _eventsDbService;
        private readonly IEncryptionService _encryptionService;
        
        public EventsService(IEventsDbService eventsDbService, IEncryptionService encryptionService)
        {
            this._eventsDbService = eventsDbService;
            this._encryptionService = encryptionService;
        }

        /// <summary>
        /// Method To Create New Event
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public string CreateEvent(Event eventData)
        {
            // Decrypt event owner's id
            eventData.Event_Owner = _encryptionService.Decrypt(eventData.Event_Owner);
            return _eventsDbService.CreateEvent(eventData);
        }

        /// <summary>
        /// Method To Fetch Event By Its ID
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event GetEventById(string eventId)
        {
            return _eventsDbService.GetEventById(eventId);
        }

        /// <summary>
        /// Method To Fetch List of All Events
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Event> GetAllEvents()
        {
            return _eventsDbService.GetAllEvents();
        }

        /// <summary>
        /// Method To Fetch List of All Events For a Particular Owner
        /// </summary>
        /// <param name="eventOwner"></param>
        /// <returns></returns>
        public IEnumerable<Event> GetEventsForOwner(string eventOwner)
        {
            // Decrypt event owner's id & send to DB Service
            return _eventsDbService.GetEventsForOwner(_encryptionService.Decrypt(eventOwner));
        }

        /// <summary>
        /// Method To Update An Event
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public bool UpdateEvent(Event eventData)
        {
            // Decrypt event owner's id
            eventData.Event_Owner = _encryptionService.Decrypt(eventData.Event_Owner);
            return _eventsDbService.UpdateEvent(eventData);
        }

        /// <summary>
        /// Method To Delete An Event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool DeleteEvent(string eventId)
        {
            return _eventsDbService.DeleteEvent(eventId);
        }
    }
}
