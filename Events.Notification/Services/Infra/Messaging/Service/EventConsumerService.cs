using Events.Notification.Services.Infra.Messaging.Config;
using Events.Notification.Services.Domain.Entities;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using System.Threading.Tasks;
using System;
using RabbitMQ.Client.Events;
using System.Threading;
using Events.Notification.Services.Infra.DB.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Events.Notification.Services.Infra.Messaging.Service{
    public class EventConsumerService: BackgroundService
    {

        private IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private readonly IEmailNotificationsDbService _dbService;

        
        public EventConsumerService(IEventMessagingConfig messagingSettings, IEmailNotificationsDbService dbService){
            this._hostname = messagingSettings.Hostname;
            this._queueName = messagingSettings.QueueName;
            this._username = messagingSettings.UserName;
            this._password = messagingSettings.Password;
            this._dbService = dbService;
            
            // Initialization of RabbitMQ Listner     
            InitializeRabbitMqListener();

            
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var notificatioModel = JsonSerializer.Deserialize<NotificationModel>(content);

                // Save notification in MongoDB
                _dbService.AddNotificationToDb(notificatioModel);
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;
            _channel.BasicConsume(_queueName, false, consumer);
            return Task.CompletedTask;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }
        
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

    }
}