using Events.Manager.Services.Domain.Entities;
using Events.Manager.Services.Infra.DB.Config;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;  
using System.Linq;

namespace Events.Tenancy.Services.Infra.DB.Service
{
    public class ParticipantsDbService : IParticipantsDbService
    {
        private readonly IMongoCollection<Participants> _participants;
        public ParticipantsDbService(IDatabaseConfig settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _participants = database.GetCollection<Participants>(settings.ParticipantsCollectionName);
        }

        // Method To Add Participants To An Event
        public void AddParticipants(Participants participantsData) {
            _participants.InsertOne(participantsData);

        }

        // Method to fetch all participants for an event
        public IEnumerable<Participants> FetchParticipantsForEvent(string eventId)
        {
            return _participants.Find<Participants>(e => e.Event_Id == eventId).ToList();
            
        }
    }
}