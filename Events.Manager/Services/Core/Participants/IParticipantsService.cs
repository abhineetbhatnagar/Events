using Events.Manager.Services.Domain.Entities;
using System.Collections.Generic;

namespace Events.Manager.Services.Core
{
    public interface IParticipantsService
    {
        // Method To Add Participants To An Event
        void AddParticipants(Participants participantsData);

        public IEnumerable<Participant> FetchParticipantsForEvent(string eventId);
    }
}