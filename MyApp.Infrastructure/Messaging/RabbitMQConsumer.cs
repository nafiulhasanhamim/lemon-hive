using System.Text;
using System.Text.Json;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MyApp.Infrastructure.Messaging
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQConsumer(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = configuration.GetSection("RabbitMQ")["HostName"],
                UserName = configuration.GetSection("RabbitMQ")["UserName"],
                Password = configuration.GetSection("RabbitMQ")["Password"],
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("sentEmail", false, false, false, null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            CreateConsumer("sentEmail", async (message) =>
            {
                Console.WriteLine("message recieved via rabbitmq");
                var eventMessage = JsonSerializer.Deserialize<MailDto>(message);
                // Console.WriteLine(eventMessage!.Content);
                if (eventMessage == null)
                {
                    return;
                }
                using (var scope = _serviceProvider.CreateScope())
                {
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    var messages = new Message(new string[] { eventMessage.To! }, eventMessage.Subject, eventMessage.Content);
                    emailService.SendEmail(messages);
                }
            });

            return Task.CompletedTask;
        }

        private void CreateConsumer(string queueName, Func<string, Task> processMessage)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await processMessage(message);
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
