using Client.Models;
using Client.Services;
using Client.ViewModels.Dialogs;
using Client.Views.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using Wpf.Ui.Abstractions.Controls;
using Wpf.Ui.Abstractions.Controls.Interfaces;
using Wpf.Ui.Controls;

namespace Client.ViewModels.Pages;

public partial class StudentsViewModel : ObservableObject, INavigationAware
{
    private readonly ApiService _apiService;
    private readonly SnackbarService _snackbar;

    [ObservableProperty]
    private ObservableCollection<Student> students = new();

    [ObservableProperty]
    private Student? selectedStudent;

    public StudentsViewModel(ApiService apiService, SnackbarService snackbar)
    {
        _apiService = apiService;
        _snackbar = snackbar;
    }

    public async Task OnNavigatedToAsync()
    {
        await LoadStudentsAsync();
    }

    public Task OnNavigatedFromAsync() => Task.CompletedTask;

    [RelayCommand]
    private async Task LoadStudentsAsync()
    {
        try
        {
            Students = new ObservableCollection<Student>(await _apiService.GetListAsync<Student>("students"));
            _snackbar.ShowSuccess("Students loaded! 👨‍🎓", null);
        }
        catch (Exception ex)
        {
            _snackbar.ShowError($"Error loading students: {ex.Message}", null);
        }
    }

    [RelayCommand]
    private async Task AddStudentAsync()
    {
        var newStudent = await ShowEditDialogAsync(new Student());
        if (newStudent != null && await _apiService.PostAsync("students", newStudent))
        {
            await LoadStudentsAsync();
            _snackbar.ShowSuccess("Student added! ➕", null);
        }
    }

    [RelayCommand]
    private async Task EditStudentAsync()
    {
        if (SelectedStudent == null) return;
        var edited = await ShowEditDialogAsync(SelectedStudent);
        if (edited != null && await _apiService.PutAsync("students", edited))
        {
            await LoadStudentsAsync();
            _snackbar.ShowSuccess("Student updated! ✏️", null);
        }
    }

    [RelayCommand]
    private async Task DeleteStudentAsync()
    {
        if (SelectedStudent == null) return;
        var result = MessageBox.Show("Delete this student?", "Confirm", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes && await _apiService.DeleteAsync("students", SelectedStudent))
        {
            await LoadStudentsAsync();
            _snackbar.ShowSuccess("Student deleted! 🗑️", null);
        }
    }

    private async Task<Student?> ShowEditDialogAsync(Student student)
    {
        var dialog = new Dialog { Title = "Edit Student" };
        var editControl = new EditDialog { Item = student };
        dialog.Content = editControl;
        var vm = (EditDialogViewModel)editControl.DataContext;
        Student? result = null;
        vm.Saved += () => { result = (Student?)editControl.Item; dialog.Hide(); };
        vm.Cancelled += () => dialog.Hide();
        await dialog.ShowAsync(Application.Current.MainWindow);
        return result;
    }
}
