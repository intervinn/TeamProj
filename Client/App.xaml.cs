using Client.Services;
using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client;

public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();
        services.AddSingleton<ApiService>();
        services.AddSingleton<Services.SnackbarService>();
        services.AddSingleton<MainViewModel>();
        ServiceProvider = services.BuildServiceProvider();

        base.OnStartup(e);
    }
}
