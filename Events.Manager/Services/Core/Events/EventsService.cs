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

        // Method To Create New Event
        public string CreateEvent(Event eventData)
        {
            // Decrypt event owner's id
            eventData.Event_Owner = _encryptionService.Decrypt(eventData.Event_Owner);
            return _eventsDbService.CreateEvent(eventData);
        }

        // Method To Fetch Event By Its ID
        public Event GetEventById(string eventId)
        {
            return _eventsDbService.GetEventById(eventId);
        }

        // Method To Fetch List of All Events
        public IEnumerable<Event> GetAllEvents()
        {
            return _eventsDbService.GetAllEvents();
        }

        // Method To Fetch List of All Events For a Particular Owner
        public IEnumerable<Event> GetEventsForOwner(string eventOwner)
        {
            // Decrypt event owner's id & send to DB Service
            return _eventsDbService.GetEventsForOwner(_encryptionService.Decrypt(eventOwner));
        }

        // Method To Update An Event
        public bool UpdateEvent(Event eventData)
        {
            // Decrypt event owner's id
            eventData.Event_Owner = _encryptionService.Decrypt(eventData.Event_Owner);
            return _eventsDbService.UpdateEvent(eventData);
        }

        // Method To Delete An Event
        public bool DeleteEvent(string eventId)
        {
            return _eventsDbService.DeleteEvent(eventId);
        }
    }
}
