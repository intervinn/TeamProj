using Client.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Views.Pages;

public partial class GradesPage : Wpf.Ui.Controls.Page
{
    public GradesPage()
    {
        InitializeComponent();
        DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<GradesViewModel>();
    }
}
