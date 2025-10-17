using Client.ViewModels.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace TeamProj.Views.Pages;

public partial class LessonsPage : Wpf.Ui.Controls.Page
{
    public LessonsPage()
    {
        InitializeComponent();
        DataContext = ((App)Application.Current).ServiceProvider.GetRequiredService<LessonsViewModel>();
    }
}
