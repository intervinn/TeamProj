using Client.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Views.Pages;

public partial class SchedulesPage : Wpf.Ui.Controls.Page
{
    public SchedulesPage()
    {
        InitializeComponent();
        DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<SchedulesViewModel>();
    }
}
