using Events.Manager.Services.Domain.Entities;
using System.Collections.Generic;

namespace Events.Tenancy.Services.Infra.DB.Service
{
    public interface IEventsDbService
    {
        // Method To Create New Event
        string CreateEvent(Event eventData);

        // Method To Fetch Event By Its ID
        Event GetEventById(string eventId);

        // Method To Fetch List of All Events
        IEnumerable<Event> GetAllEvents();

        // Method To Fetch List of All Events For a Particular Owner
        IEnumerable<Event> GetEventsForOwner(string eventOwner);

        // Method To Update An Event
        bool UpdateEvent(Event eventData);

        // Method To Delete An Event
        bool DeleteEvent(string eventId);
    }
}