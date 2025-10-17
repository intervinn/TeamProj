using Wpf.Ui.Controls;

namespace Client.Services;

public class SnackbarService
{
    public void ShowSuccess(string message, string? actionContent = null)
    {
        var snackbar = new SnackbarQueue();
        snackbar.Enqueue(new SnackbarQueueItem
        {
            Title = "Success",
            Message = message,
            Appearance = ControlAppearance.Success,
            ActionContent = actionContent
        });
    }

    public void ShowError(string message, string? actionContent = null)
    {
        var snackbar = new SnackbarQueue();
        snackbar.Enqueue(new SnackbarQueueItem
        {
            Title = "Error",
            Message = message,
            Appearance = ControlAppearance.Danger,
            ActionContent = actionContent
        });
    }
}
