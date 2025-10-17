using Client.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Views.Pages;

public partial class StudentsPage : Wpf.Ui.Controls.Page
{
    public StudentsPage()
    {
        InitializeComponent();
        DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<StudentsViewModel>();
    }
}
