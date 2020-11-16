using Events.Manager.Services.Domain.Entities;
using Events.Tenancy.Services.Infra.DB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Manager.Services.Core
{
    public class ParticipantsService : IParticipantsService
    {
        private readonly IParticipantsDbService _participantsDbService;
        public ParticipantsService(IParticipantsDbService participantsDbService)
        {
            this._participantsDbService = participantsDbService;
        }

        /// <summary>
        /// Method To Add Participants To An Event
        /// </summary>
        /// <param name="participantsData"></param>
        public void AddParticipants(Participants participantsData)
        {
            _participantsDbService.AddParticipants(participantsData);
        }

        /// <summary>
        /// Method to fetch all participants for an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public IEnumerable<Participant> FetchParticipantsForEvent(string eventId) {
            List<Participant> listOfParticipants = new List<Participant>();
            IEnumerable<Participants> _allParticipantDocuments = _participantsDbService.FetchParticipantsForEvent(eventId);
            foreach (var participantEntry in _allParticipantDocuments) {
                foreach (var participant in participantEntry.ParticipantsData)
                {
                    listOfParticipants.Add(participant);
                }
            }
            return listOfParticipants;
        }
    }
}
