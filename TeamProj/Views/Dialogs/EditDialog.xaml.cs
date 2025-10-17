using Client.ViewModels.Dialogs;
using System.Windows.Controls;

namespace TeamProj.Views.Dialogs;

public partial class EditDialog : UserControl
{
    public object? Item
    {
        get => ((EditDialogViewModel)DataContext).Item;
        set => ((EditDialogViewModel)DataContext).Item = value;
    }

    public EditDialog()
    {
        InitializeComponent();
        DataContext = new EditDialogViewModel();
    }
}
