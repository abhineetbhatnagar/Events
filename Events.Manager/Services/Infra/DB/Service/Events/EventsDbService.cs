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

        /// <summary>
        /// Method To Create New Event
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public string CreateEvent(Event eventData)
        {
            _events.InsertOne(eventData);
            return eventData._id;
        }

        /// <summary>
        /// Method To Fetch Event By Its ID
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event GetEventById(string eventId) {
            return _events.Find<Event>(e => e._id == eventId).FirstOrDefault();
        }

        /// <summary>
        /// Method To Fetch List of All Events
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Event> GetAllEvents() {
            return _events.Find<Event>(f => true).ToList();
        }

        /// <summary>
        /// Method To Fetch List of All Events For a Particular Owner
        /// </summary>
        /// <param name="eventOwner"></param>
        /// <returns></returns>
        public IEnumerable<Event> GetEventsForOwner(string eventOwner)
        {
            return _events.Find<Event>(e => e.Event_Owner == eventOwner).ToList();
        }

        /// <summary>
        /// Method To Update An Event
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public bool UpdateEvent(Event eventData) {
            ReplaceOneResult updateResult = _events.ReplaceOne(filter: g => g._id == eventData._id, replacement: eventData);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Method To Delete An Event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool DeleteEvent(string eventId) {
            DeleteResult deleteResult = _events.DeleteOne(filter: g => g._id == eventId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

    }
}