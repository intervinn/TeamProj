
using RabbitMQ.Client;
using Shared.Models;
using System.Text;
using System.Text.Json;

namespace Api.Services
{
    public class MessageProduceService : IHostedService, IDisposable
    {
        private ILogger<MessageProduceService> _logger;
        private IConfiguration _configuration;
        private IConnection? _connection;
        private IChannel? _channel;

        private readonly string _channelName;
        
        public MessageProduceService(ILogger<MessageProduceService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _channelName = _configuration.GetSection("RabbitMQ")["ChannelName"] ?? "teamproj_queue";
        }

        public async Task SendAsync(Message message)
        {
            if (_channel is not { IsOpen: true })
            {
                _logger.LogWarning("Попытка отправить сообщение, но канал закрыт");
                throw new Exception("Канал закрыт");
            }

            try
            {
                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                await _channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: _channelName,
                    body: body
                );
            } catch (Exception e)
            {
                _logger.LogError(e, "Не удалось отправить сообщение");
                throw;
            }
        }

        public async Task StartAsync(CancellationToken token)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _configuration.GetSection("RabbitMQ")["HostName"] ?? throw new Exception("HostName обязателен"),
                    UserName = _configuration.GetSection("RabbitMQ")["UserName"] ?? string.Empty,
                    Password = _configuration.GetSection("RabbitMQ")["Password"] ?? string.Empty
                };

                _connection = await factory.CreateConnectionAsync(token);
                _channel = await _connection.CreateChannelAsync(null, token);

                await _channel.QueueDeclareAsync(
                    _channelName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    cancellationToken: token
                );


            } catch (Exception e)
            {
                _logger.LogError(e, "Не удалось инициализировать MessageProduceService");
            }
        }

        public async Task StopAsync(CancellationToken token)
        {
            if (_connection != null)
            {
                await _connection.CloseAsync();
            }
            if (_channel != null)
            {
                await _channel.CloseAsync();
            }
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }
    }
}
