using Events.Manager.Services.Domain.Entities;
using Events.Tenancy.Services.Infra.DB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.Tenancy.Services.Infra.Messaging.Service;

namespace Events.Manager.Services.Core
{
    public class ParticipantsService : IParticipantsService
    {
        private readonly IParticipantsDbService _participantsDbService;
        private readonly IEventMessagingService _eventMessager;
        public ParticipantsService(IParticipantsDbService participantsDbService, IEventMessagingService eventMessager)
        {
            this._participantsDbService = participantsDbService;
            this._eventMessager = eventMessager;
        }

        // Method To Add Participants To An Event
        public async Task AddParticipants(Participants participantsData)
        {
            if(await _participantsDbService.AddParticipants(participantsData)){
                foreach(var participant in participantsData.ParticipantsData){
                    await _eventMessager.NotifyParticipant(new Notification{
                        Email = participant.Email,
                        Notification_Text = "You have been invited to attend an event. The event ID is: - ." + participantsData.Event_Id
                    });
                }
            }
        }

        // Method to fetch all participants for an event
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
