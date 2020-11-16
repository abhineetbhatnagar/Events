using Events.Manager.Services.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events.Tenancy.Services.Infra.DB.Service
{
    public interface IParticipantsDbService
    {
        // Method To Add Participants To An Event
        Task<bool> AddParticipants(Participants participantsData);

        // Method to fetch all participants for an event
        IEnumerable<Participants> FetchParticipantsForEvent(string eventId);
    }
}