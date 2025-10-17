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

public partial class LessonsViewModel : ObservableObject, INavigationAware
{
    private readonly ApiService _apiService;
    private readonly SnackbarService _snackbar;

    [ObservableProperty]
    private ObservableCollection<Lesson> lessons = new();

    [ObservableProperty]
    private Lesson? selectedLesson;

    public LessonsViewModel(ApiService apiService, SnackbarService snackbar)
    {
        _apiService = apiService;
        _snackbar = snackbar;
    }

    public async Task OnNavigatedToAsync()
    {
        await LoadLessonsAsync();
    }

    public Task OnNavigatedFromAsync() => Task.CompletedTask;

    [RelayCommand]
    private async Task LoadLessonsAsync()
    {
        try
        {
            Lessons = new ObservableCollection<Lesson>(await _apiService.GetListAsync<Lesson>("lessons"));
            _snackbar.ShowSuccess("Lessons loaded! 📖", null);
        }
        catch (Exception ex)
        {
            _snackbar.ShowError($"Error loading lessons: {ex.Message}", null);
        }
    }

    [RelayCommand]
    private async Task AddLessonAsync()
    {
        var newLesson = await ShowEditDialogAsync(new Lesson());
        if (newLesson != null && await _apiService.PostAsync("lessons", newLesson))
        {
            await LoadLessonsAsync();
            _snackbar.ShowSuccess("Lesson added! ➕", null);
        }
    }

    [RelayCommand]
    private async Task EditLessonAsync()
    {
        if (SelectedLesson == null) return;
        var edited = await ShowEditDialogAsync(SelectedLesson);
        if (edited != null && await _apiService.PutAsync("lessons", edited))
        {
            await LoadLessonsAsync();
            _snackbar.ShowSuccess("Lesson updated! ✏️", null);
        }
    }

    [RelayCommand]
    private async Task DeleteLessonAsync()
    {
        if (SelectedLesson == null) return;
        var result = MessageBox.Show("Delete this lesson?", "Confirm", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes && await _apiService.DeleteAsync("lessons", SelectedLesson))
        {
            await LoadLessonsAsync();
            _snackbar.ShowSuccess("Lesson deleted! 🗑️", null);
        }
    }

    private async Task<Lesson?> ShowEditDialogAsync(Lesson lesson)
    {
        var dialog = new Dialog { Title = "Edit Lesson" };
        var editControl = new EditDialog { Item = lesson };
        dialog.Content = editControl;
        var vm = (EditDialogViewModel)editControl.DataContext;
        Lesson? result = null;
        vm.Saved += () => { result = (Lesson?)editControl.Item; dialog.Hide(); };
        vm.Cancelled += () => dialog.Hide();
        await dialog.ShowAsync(Application.Current.MainWindow);
        return result;
    }
}
