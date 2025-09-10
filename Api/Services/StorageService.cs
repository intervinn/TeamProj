using Api.Models;

namespace Api.Services;

public class StorageService : IHostedService
{
    private AppDbContext _context;
    private IConfiguration _configuration;

    public AppDbContext Context
    {
        get => _context;
    }

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