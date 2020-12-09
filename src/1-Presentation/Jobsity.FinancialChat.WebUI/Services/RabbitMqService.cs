using System;
using System.Text;
using Jobsity.FinancialChat.WebUI.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Jobsity.FinancialChat.WebUI.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly ILogger<RabbitMqService> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IHubContext<ChatHub> _hub;
        private readonly string _queueName;
        private const string ExchangeName = "exchange_stock_quotes";

        public RabbitMqService(IConfiguration configuration, ILogger<RabbitMqService> logger, IHubContext<ChatHub> hub)
        {
            var hostName = configuration["RabbitMqHostName"];
            _queueName = configuration["RabbitMqChatRoomQueueName"];

            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _logger = logger;
            _hub = hub;
        }

        public void ReceiveMessageFromWorker()
        {
            _channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");

            _channel.QueueDeclare(queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(queue: _queueName,
                exchange: ExchangeName,
                routingKey: "chat_room");

            _logger.LogInformation($"Waiting for messages.");

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;

                    _logger.LogInformation($"Received '{routingKey}':'{message}");

                    await _hub.Clients.All.SendAsync("SendMessageAsync", message, "Wall Street BOT", DateTime.Now);

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} | {ex.StackTrace}");
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            _channel.BasicConsume(queue: _queueName,
                autoAck: false,
                consumer: consumer);
        }

        public void PushMessageToWorker(string message)
        {
            using var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: ExchangeName,
                routingKey: "stock_quotes_requests",
                basicProperties: null,
                body: body);

            _logger.LogInformation($"Sent '{message}' to the worker");
        }
    }
}