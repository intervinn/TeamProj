using Client.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Views.Pages;

public partial class TeachersPage : Wpf.Ui.Controls.Page
{
    public TeachersPage()
    {
        InitializeComponent();
        DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<TeachersViewModel>();
    }
}
