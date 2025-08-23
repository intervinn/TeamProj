using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Server
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                   
                })
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.json");
                })
                .ConfigureServices(services =>
                {

                })
                .Build();
            await host.StartAsync();
        }
    }
}
