using TeamProj.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace TeamProj.Views.Pages;

public partial class TeachersPage : Page
{
    public TeachersPage()
    {
        InitializeComponent();
        DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<TeachersViewModel>();
    }
}
