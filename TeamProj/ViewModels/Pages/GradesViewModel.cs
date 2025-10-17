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

public partial class GradesViewModel : ObservableObject, INavigationAware
{
    private readonly ApiService _apiService;
    private readonly SnackbarService _snackbar;

    [ObservableProperty]
    private ObservableCollection<Grade> grades = new();

    [ObservableProperty]
    private Grade? selectedGrade;

    public GradesViewModel(ApiService apiService, SnackbarService snackbar)
    {
        _apiService = apiService;
        _snackbar = snackbar;
    }

    public async Task OnNavigatedToAsync()
    {
        await LoadGradesAsync();
    }

    public Task OnNavigatedFromAsync() => Task.CompletedTask;

    [RelayCommand]
    private async Task LoadGradesAsync()
    {
        try
        {
            Grades = new ObservableCollection<Grade>(await _apiService.GetListAsync<Grade>("grades"));
            _snackbar.ShowSuccess("Grades loaded! 📊", null);
        }
        catch (Exception ex)
        {
            _snackbar.ShowError($"Error loading grades: {ex.Message}", null);
        }
    }

    [RelayCommand]
    private async Task AddGradeAsync()
    {
        var newGrade = await ShowEditDialogAsync(new Grade());
        if (newGrade != null && await _apiService.PostAsync("grades", newGrade))
        {
            await LoadGradesAsync();
            _snackbar.ShowSuccess("Grade added! ➕", null);
        }
    }

    [RelayCommand]
    private async Task EditGradeAsync()
    {
        if (SelectedGrade == null) return;
        var edited = await ShowEditDialogAsync(SelectedGrade);
        if (edited != null && await _apiService.PutAsync("grades", edited))
        {
            await LoadGradesAsync();
            _snackbar.ShowSuccess("Grade updated! ✏️", null);
        }
    }

    [RelayCommand]
    private async Task DeleteGradeAsync()
    {
        if (SelectedGrade == null) return;
        var result = MessageBox.Show("Delete this grade?", "Confirm", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes && await _apiService.DeleteAsync("grades", SelectedGrade))
        {
            await LoadGradesAsync();
            _snackbar.ShowSuccess("Grade deleted! 🗑️", null);
        }
    }

    private async Task<Grade?> ShowEditDialogAsync(Grade grade)
    {
        var dialog = new Dialog { Title = "Edit Grade" };
        var editControl = new EditDialog { Item = grade };
        dialog.Content = editControl;
        var vm = (EditDialogViewModel)editControl.DataContext;
        Grade? result = null;
        vm.Saved += () => { result = (Grade?)editControl.Item; dialog.Hide(); };
        vm.Cancelled += () => dialog.Hide();
        await dialog.ShowAsync(Application.Current.MainWindow);
        return result;
    }
}
