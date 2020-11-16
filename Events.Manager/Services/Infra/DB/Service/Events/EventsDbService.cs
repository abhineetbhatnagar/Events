using Events.Manager.Services.Domain.Entities;
using Events.Manager.Services.Infra.DB.Config;
using MongoDB.Driver;  
using System.Collections.Generic;  
using System.Linq;

namespace Events.Tenancy.Services.Infra.DB.Service
{
    public class EventsDbService : IEventsDbService
    {
        private readonly IMongoCollection<Event> _events;
        public EventsDbService(IDatabaseConfig settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _events = database.GetCollection<Event>(settings.EventsCollectionName);
        }

        // Method To Create New Event
        public string CreateEvent(Event eventData)
        {
            _events.InsertOne(eventData);
            return eventData._id;
        }

        // Method To Fetch Event By Its ID
        public Event GetEventById(string eventId) {
            return _events.Find<Event>(e => e._id == eventId).FirstOrDefault();
        }

        // Method To Fetch List of All Events
        public IEnumerable<Event> GetAllEvents() {
            return _events.Find<Event>(f => true).ToList();
        }

        // Method To Fetch List of All Events For a Particular Owner
        public IEnumerable<Event> GetEventsForOwner(string eventOwner)
        {
            return _events.Find<Event>(e => e.Event_Owner == eventOwner).ToList();
        }

        // Method To Update An Event
        public bool UpdateEvent(Event eventData) {
            ReplaceOneResult updateResult = _events.ReplaceOne(filter: g => g._id == eventData._id, replacement: eventData);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        // Method To Delete An Event
        public bool DeleteEvent(string eventId) {
            DeleteResult deleteResult = _events.DeleteOne(filter: g => g._id == eventId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

    }
}