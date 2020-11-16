using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Events.Manager.Services.Domain.Entities
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Event_Owner { get; set; }
        public string Event_Name { get; set; }
        public string Event_Description { get; set; }
        public string Event_Venue { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? Event_Start_Date { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? Event_End_Date { get; set; }
    }
}
