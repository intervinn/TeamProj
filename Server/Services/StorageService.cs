
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Models;

namespace Server.Services
{
    public class StorageService : IHostedService
    {
        private AppDbContext _context;
        private IConfiguration _configuration;
        private ILogger<StorageService> _logger;

        public AppDbContext Context
        {
            get => _context;
        }

        public StorageService(IConfiguration configuration, ILogger<StorageService> logger)
        {
            _configuration = configuration;
            _context = new(_configuration);
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken token)
        {
            await _context.Database.EnsureCreatedAsync();
            _logger.LogInformation("Установлено подключение к базе данных");
        }

        public async Task StopAsync(CancellationToken token)
        {
            await Task.CompletedTask;
        }
    }
}
