using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jobsity.FinancialChat.Application.Common.Interfaces.ExternalApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Jobsity.FinancialChat.StockQuoteBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;
        private const string ExchangeName = "exchange_stock_quotes";
        private readonly IStooqApi _stooqApi;

        public Worker(IConfiguration configuration, ILogger<Worker> logger, IStooqApi stooqApi)
        {
            var hostName = configuration["RabbitMqHostName"];
            _queueName = configuration["RabbitMqStockQuotesQueueName"];

            var factory = new ConnectionFactory() { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _logger = logger;
            _stooqApi = stooqApi;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");

            _channel.QueueDeclare(queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueBind(queue: _queueName,
                exchange: ExchangeName,
                routingKey: "stock_quotes_requests");

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

                    var stockQuote = await _stooqApi.GetStockQuote(message);

                    PushMessageToChatRoom(stockQuote);

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

        private void PushMessageToChatRoom(string message)
        {
            using var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: ExchangeName,
                routingKey: "chat_room",
                basicProperties: null,
                body: body);

            _logger.LogInformation($"Sent '{message}' to chat room");
        }
    }
}
