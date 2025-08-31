using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Server.Handlers;
using Server.Models;
using Shared.Models;
using System.Text;
using System.Text.Json;

namespace Server.Services
{
    class MessageConsumeService : IHostedService, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MessageConsumeService> _logger;
        private readonly StorageService _storage;
        private readonly IServiceProvider _provider;

        private IChannel? _channel;
        private IConnection? _connection;

        public MessageConsumeService(
            IConfiguration configuration,
            ILogger<MessageConsumeService> logger,
            StorageService storage,
            IServiceProvider provider
        )
        {
            _configuration = configuration;
            _logger = logger;
            _storage = storage;
            _provider = provider;
        }

        private IMessageHandler? GetMessageHandler(string model)
        => model switch
        {
            "Grade" => _provider.GetService(typeof(GradeHandler)) as IMessageHandler,
            "Lesson" => _provider.GetService(typeof(LessonHandler)) as IMessageHandler,
            "Schedule" => _provider.GetService(typeof(ScheduleHandler)) as IMessageHandler,
            "Student" => _provider.GetService(typeof(StudentHandler)) as IMessageHandler,
            "Teacher" => _provider.GetService(typeof(TeacherHandler)) as IMessageHandler,
            _ => null
        };

        private async Task HandleAction(IMessageHandler handler, string action, object data)
        {
            switch (action)
            {
                case "Create":
                    await handler.CreateAsync(data);
                    break;
                case "Delete":
                    await handler.DeleteAsync(data);
                    break;
                case "Edit":
                    await handler.EditAsync(data);
                    break;
                default:
                    throw new Exception($"Неизвестное действие: {action}");
            }
        }

        private async Task OnReceivedAsync(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                var body = Encoding.UTF8.GetString(args.Body.ToArray());
                try
                {
                    var message = JsonSerializer.Deserialize<Message>(body);
                    if (message == null)
                    {
                        throw new Exception("Данные не совпадают со схемой");
                    }

                    var handler = GetMessageHandler(message.ModelType);
                    if (handler == null)
                    {
                        throw new Exception($"Неизвестная модель: {message.ModelType}");
                    }

                    await HandleAction(handler, message.Action, message.Data);
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
                _logger.LogError($"Не удалось инициализировать MessageConsumeService: {e}");
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
