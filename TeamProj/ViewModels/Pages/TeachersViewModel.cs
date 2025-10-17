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

namespace TeamProj.ViewModels.Pages;

public partial class TeachersViewModel : ObservableObject, INavigationAware
{
    private readonly ApiService _apiService;
    private readonly SnackbarService _snackbar;

    [ObservableProperty]
    private ObservableCollection<Teacher> teachers = new();

    [ObservableProperty]
    private Teacher? selectedTeacher;

    public TeachersViewModel(ApiService apiService, SnackbarService snackbar)
    {
        _apiService = apiService;
        _snackbar = snackbar;
    }

    public async Task OnNavigatedToAsync()
    {
        await LoadTeachersAsync();
    }

    public Task OnNavigatedFromAsync() => Task.CompletedTask;

    [RelayCommand]
    private async Task LoadTeachersAsync()
    {
        try
        {
            Teachers = new ObservableCollection<Teacher>(await _apiService.GetListAsync<Teacher>("teachers"));
            _snackbar.ShowSuccess("Teachers loaded! 👩‍🏫", null);
        }
        catch (Exception ex)
        {
            _snackbar.ShowError($"Error loading teachers: {ex.Message}", null);
        }
    }

    [RelayCommand]
    private async Task AddTeacherAsync()
    {
        var newTeacher = await ShowEditDialogAsync(new Teacher());
        if (newTeacher != null && await _apiService.PostAsync("teachers", newTeacher))
        {
            await LoadTeachersAsync();
            _snackbar.ShowSuccess("Teacher added! ➕", null);
        }
    }

    [RelayCommand]
    private async Task EditTeacherAsync()
    {
        if (SelectedTeacher == null) return;
        var edited = await ShowEditDialogAsync(SelectedTeacher);
        if (edited != null && await _apiService.PutAsync("teachers", edited))
        {
            await LoadTeachersAsync();
            _snackbar.ShowSuccess("Teacher updated! ✏️", null);
        }
    }

    [RelayCommand]
    private async Task DeleteTeacherAsync()
    {
        if (SelectedTeacher == null) return;
        var result = MessageBox.Show("Delete this teacher?", "Confirm", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes && await _apiService.DeleteAsync("teachers", SelectedTeacher))
        {
            await LoadTeachersAsync();
            _snackbar.ShowSuccess("Teacher deleted! 🗑️", null);
        }
    }

    private async Task<Teacher?> ShowEditDialogAsync(Teacher teacher)
    {
        var dialog = new Dialog { Title = "Edit Teacher" };
        var editControl = new EditDialog { Item = teacher };
        dialog.Content = editControl;
        var vm = (EditDialogViewModel)editControl.DataContext;
        Teacher? result = null;
        vm.Saved += () => { result = (Teacher?)editControl.Item; dialog.Hide(); };
        vm.Cancelled += () => dialog.Hide();
        await dialog.ShowAsync(Application.Current.MainWindow);
        return result;
    }
}
