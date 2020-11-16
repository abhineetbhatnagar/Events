using Events.Manager.Services.Domain.Entities;
using System.Collections.Generic;

namespace Events.Manager.Services.Core
{
    public interface IParticipantsService
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
        public IEnumerable<Participant> FetchParticipantsForEvent(string eventId);
    }
}