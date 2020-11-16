using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Events.Manager.Services.Domain.Entities
{
    public class Participants
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide event id.")]
        public string Event_Id { get; set; }
        [Required]
        public List<Participant> ParticipantsData { get; set; }
    }

    public class Participant
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide participant name.")]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide participant email.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
