using Client.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Client;

public partial class MainWindow : Wpf.Ui.Controls.Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<MainViewModel>();
    }
}
