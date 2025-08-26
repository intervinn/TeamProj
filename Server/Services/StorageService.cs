
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Server.Models;

namespace Server.Services
{
    public class StorageService : IHostedService
    {
        private AppDbContext _context;
        private IConfiguration _configuration;

        public StorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            _context = new(_configuration);
        }

        public async Task StartAsync(CancellationToken token)
        {
            await _context.Database.EnsureCreatedAsync();
        }

        public async Task StopAsync(CancellationToken token)
        {
            await Task.CompletedTask;
        }
    }
}
