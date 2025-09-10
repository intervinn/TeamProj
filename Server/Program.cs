using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Handlers;
using Server.Services;

namespace Server
{
    internal class Program
    {
        public static async Task Main(string[] args)
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
                    services.AddHostedService<StorageService>();
                    services.AddSingleton<GradeHandler>();
                    services.AddSingleton<StudentHandler>();
                    services.AddSingleton<TeacherHandler>();
                    services.AddSingleton<ScheduleHandler>();
                    services.AddSingleton<LessonHandler>();
                    services.AddHostedService<MessageConsumeService>();
                })
                .Build();
            await host.RunAsync();
        }
    }
}
