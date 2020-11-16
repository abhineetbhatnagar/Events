using Events.Manager.Services.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Events.Tenancy.Services.Infra.DB.Service
{
    public interface IParticipantsDbService
    {
        /// <summary>
        /// Method To Add Participants To An Event
        /// </summary>
        /// <param name="participantsData"></param>
        void AddParticipants(Participants participantsData);

        /// <summary>
        /// Method to fetch all participants for an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        IEnumerable<Participants> FetchParticipantsForEvent(string eventId);
    }
}