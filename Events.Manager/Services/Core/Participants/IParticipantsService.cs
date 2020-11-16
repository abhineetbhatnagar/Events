using Events.Manager.Services.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events.Manager.Services.Core
{
    public interface IParticipantsService
    {
        // Method To Add Participants To An Event
        Task AddParticipants(Participants participantsData);

        // Method to fetch all participants for an event
        public IEnumerable<Participant> FetchParticipantsForEvent(string eventId);
    }
}