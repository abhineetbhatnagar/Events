using Events.Manager.Services.Infra.Messaging.Config;
using Events.Manager.Services.Domain.Entities;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using System.Threading.Tasks;

namespace Events.Tenancy.Services.Infra.Messaging.Service{
    public class EventMessagingService: IEventMessagingService{

        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        public EventMessagingService(IEventMessagingConfig messagingSettings){
            _hostname = messagingSettings.Hostname;
            _queueName = messagingSettings.QueueName;
            _username = messagingSettings.UserName;
            _password = messagingSettings.Password;
        }

        // Method for publishing a message on Event Bus
        // So that participants can be notified
        public async Task<bool> NotifyParticipant(Notification notification)
        {
            var factory = new ConnectionFactory() { HostName = _hostname, UserName = _username, Password = _password };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var json = JsonSerializer.Serialize(notification);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
                await Task.Yield();
            }
            return true;
        }
    }
}