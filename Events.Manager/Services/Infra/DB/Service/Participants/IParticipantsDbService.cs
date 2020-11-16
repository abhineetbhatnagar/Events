using Events.Manager.Services.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events.Tenancy.Services.Infra.DB.Service
{
    public interface IParticipantsDbService
    {
        // Method To Add Participants To An Event
        void AddParticipants(Participants participantsData);
        IEnumerable<Participants> FetchParticipantsForEvent(string eventId);
    }
}