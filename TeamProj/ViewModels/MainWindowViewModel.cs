using Client.Views.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

namespace Client.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly Frame _mainFrame;

    public MainViewModel()
    {
        _mainFrame = (Application.Current.MainWindow as MainWindow)?.MainFrame;
    }

    [RelayCommand]
    private void NavigateToStudents() => _mainFrame.Navigate(new StudentsPage());

    [RelayCommand]
    private void NavigateToTeachers() => _mainFrame.Navigate(new TeachersPage());

    [RelayCommand]
    private void NavigateToGrades() => _mainFrame.Navigate(new GradesPage());

    [RelayCommand]
    private void NavigateToLessons() => _mainFrame.Navigate(new LessonsPage());

    [RelayCommand]
    private void NavigateToSchedules() => _mainFrame.Navigate(new SchedulesPage());
}
