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

public partial class SchedulesViewModel : ObservableObject, INavigationAware
{
    private readonly ApiService _apiService;
    private readonly SnackbarService _snackbar;

    [ObservableProperty]
    private ObservableCollection<Schedule> schedules = new();

    [ObservableProperty]
    private Schedule? selectedSchedule;

    public SchedulesViewModel(ApiService apiService, SnackbarService snackbar)
    {
        _apiService = apiService;
        _snackbar = snackbar;
    }

    public async Task OnNavigatedToAsync()
    {
        await LoadSchedulesAsync();
    }

    public Task OnNavigatedFromAsync() => Task.CompletedTask;

    [RelayCommand]
    private async Task LoadSchedulesAsync()
    {
        try
        {
            Schedules = new ObservableCollection<Schedule>(await _apiService.GetListAsync<Schedule>("schedules"));
            _snackbar.ShowSuccess("Schedules loaded! 📅", null);
        }
        catch (Exception ex)
        {
            _snackbar.ShowError($"Error loading schedules: {ex.Message}", null);
        }
    }

    [RelayCommand]
    private async Task AddScheduleAsync()
    {
        var newSchedule = await ShowEditDialogAsync(new Schedule());
        if (newSchedule != null && await _apiService.PostAsync("schedules", newSchedule))
        {
            await LoadSchedulesAsync();
            _snackbar.ShowSuccess("Schedule added! ➕", null);
        }
    }

    [RelayCommand]
    private async Task EditScheduleAsync()
    {
        if (SelectedSchedule == null) return;
        var edited = await ShowEditDialogAsync(SelectedSchedule);
        if (edited != null && await _apiService.PutAsync("schedules", edited))
        {
            await LoadSchedulesAsync();
            _snackbar.ShowSuccess("Schedule updated! ✏️", null);
        }
    }

    [RelayCommand]
    private async Task DeleteScheduleAsync()
    {
        if (SelectedSchedule == null) return;
        var result = MessageBox.Show("Delete this schedule?", "Confirm", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes && await _apiService.DeleteAsync("schedules", SelectedSchedule))
        {
            await LoadSchedulesAsync();
            _snackbar.ShowSuccess("Schedule deleted! 🗑️", null);
        }
    }

    private async Task<Schedule?> ShowEditDialogAsync(Schedule schedule)
    {
        var dialog = new Dialog { Title = "Edit Schedule" };
        var editControl = new EditDialog { Item = schedule };
        dialog.Content = editControl;
        var vm = (EditDialogViewModel)editControl.DataContext;
        Schedule? result = null;
        vm.Saved += () => { result = (Schedule?)editControl.Item; dialog.Hide(); };
        vm.Cancelled += () => dialog.Hide();
        await dialog.ShowAsync(Application.Current.MainWindow);
        return result;
    }
}
