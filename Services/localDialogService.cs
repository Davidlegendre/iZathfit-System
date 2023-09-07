using Commons;
using Models.ModelsCommons;
using Wpf.Ui.Controls;

namespace Services;

public class localDialogService
{
    public bool ShowDialog(DialogModel dialogModel, UiWindow? owner = null) {
        var dialog = new Commons.Dialog(dialogModel);
        if(owner != null) dialog.Owner = owner;
        return dialog.ShowDialog()!.Value;
    }
}
