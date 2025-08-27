using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Server.Models;
using System.Text;
using System.Text.Json;

namespace Server.Services
{
    class MessageConsumeService : IHostedService, IDisposable
    {
        private IConfiguration _configuration;
        private ILogger<MessageConsumeService> _logger;
        private StorageService _storage;

        private IChannel? _channel;
        private IConnection? _connection;
        

        public MessageConsumeService(
            IConfiguration configuration,
            ILogger<MessageConsumeService> logger,
            StorageService storage
        )
        {
            _configuration = configuration;
            _logger = logger;
            _storage = storage;
        }

        private async Task OnReceivedAsync(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                var body = Encoding.UTF8.GetString(args.Body.ToArray());
                try
                {
                    var message = JsonSerializer.Deserialize<Message>(body);

                } catch (Exception e)
                {
                    _logger.LogWarning($"Сообщение не получилось десериализовать: {e}");
                }
            } catch (Exception e)
            {
                _logger.LogError($"Ошибка при получении сообщение: {e}");
            }
        }

        public async Task StartAsync(CancellationToken token)
        {
            try
            {
                ConnectionFactory factory = new()
                {
                    HostName = _configuration.GetSection("RabbitMQ")["HostName"] ?? throw new Exception("HostName обязателен"),
                    UserName = _configuration.GetSection("RabbitMQ")["UserName"] ?? string.Empty,
                    Password = _configuration.GetSection("RabbitMQ")["Password"] ?? string.Empty
                };

                _connection = await factory.CreateConnectionAsync(token);
                _channel = await _connection.CreateChannelAsync(null, token);

                var channelName 
                    = _configuration.GetSection("RabbitMQ")["ChannelName"] ?? "teamproj_queue";

                await _channel.QueueDeclareAsync(
                    channelName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    cancellationToken: token
                );

                AsyncEventingBasicConsumer consumer = new(_channel);
                consumer.ReceivedAsync += OnReceivedAsync;
                await _channel.BasicConsumeAsync(channelName, true, consumer);
            } catch (Exception e)
            {
                
            }
        }

        public async Task StopAsync(CancellationToken token)
        {
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
