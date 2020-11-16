using Events.Manager.Services.Domain.Entities;
using System.Collections.Generic;

namespace Events.Manager.Services.Core
{
    public interface IEventsService
    {
        string CreateEvent(Event eventData);
        bool DeleteEvent(string eventId);
        IEnumerable<Event> GetAllEvents();
        Event GetEventById(string eventId);
        IEnumerable<Event> GetEventsForOwner(string eventOwner);
        bool UpdateEvent(Event eventData);
    }
}