using Client.Services;
using Wpf.Ui;
using Client.ViewModels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Client;

public partial class Program
{
    [STAThread]
    public static void Main(object host)
    {
        var app = new Application
        {
            // Enable dark theme by default
            Resources = new ResourceDictionary
            {
                Source = new Uri("/Wpf.Ui;component/Styles/Theme/Dark.xaml", UriKind.Relative)
            }
        };

        var appBuilder = Host.CreateDefaultBuilder()
            .ConfigureServices(static (context, services) =>
            {
                // WPF-UI Services
                services.AddWpfUi();

                // App Host
                services.AddHostedService<ApplicationHostService>();

                // ViewModels
                services.AddTransient<MainWindowViewModel>();

                // Services
                services.AddHttpClient<ApiService>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7001/"); // Adjust API URL
                });
                services.AddSingleton<SnackbarService>();

                // Views
                services.AddTransient<MainWindow>(provider =>
                {
                    var serviceProvider = provider.GetRequiredService<IServiceProvider>();
                    var window = serviceProvider.GetRequiredService<MainWindow>();
                    window.DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>();
                    return window;
                });
            })
            .Build();

        app.Run(appBuilder.Services.GetRequiredService<MainWindow>());
    }
}

