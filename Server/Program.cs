using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Handlers;
using Server.Services;

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
                    services.AddSingleton<GradeHandler>();
                    services.AddSingleton<StudentHandler>();
                    services.AddSingleton<TeacherHandler>();
                    services.AddSingleton<ScheduleHandler>();
                    services.AddSingleton<LessonHandler>();

                    services.AddHostedService<StorageService>();
                    services.AddHostedService<MessageConsumeService>();
                })
                .Build();
            await host.StartAsync();
        }
    }
}
