using Commons;
using Models.ModelsCommons;


namespace Services;

public class localDialogService
{
    public bool ShowDialog(DialogModel dialogModel) { 
        return new Dialog(dialogModel).ShowDialog()!.Value;
    }
}
