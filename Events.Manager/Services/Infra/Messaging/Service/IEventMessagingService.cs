using System.Threading.Tasks;
using Events.Manager.Services.Domain.Entities;
namespace Events.Tenancy.Services.Infra.Messaging.Service{
    public interface IEventMessagingService{
        
        // Method for publishing a message on Event Bus
        // So that participants can be notified
        Task NotifyParticipant(Notification notification);
    }
}