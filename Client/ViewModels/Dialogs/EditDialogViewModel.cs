using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace Client.ViewModels.Dialogs;

public partial class EditDialogViewModel : ObservableObject
{
    [ObservableProperty]
    private object? item;

    public event EventHandler? Saved;
    public event EventHandler? Cancelled;

    [RelayCommand]
    private void Save() => Saved?.Invoke(this, EventArgs.Empty);

    [RelayCommand]
    private void Cancel() => Cancelled?.Invoke(this, EventArgs.Empty);
}
