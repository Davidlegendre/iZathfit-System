using Commons;
using Dapper;
using Models.ModelsCommons;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Documents;
using Wpf.Ui.Controls;

namespace Services;

public class localDialogService
{
    public bool ShowDialog(string mensaje, string titulo = "Mensaje",
            bool ShowCancelButton = false,
          UiWindow? owner = null,
            string aceptarbutton = "Aceptar", string cancelarButton = "Cancelar",
            LinkModel[]? links = null) {

        var dialog = new Commons.Dialog(new DialogModel()
        {
            Title = titulo,
            Message = mensaje,
             canShowCancelButton = ShowCancelButton,
            aceptarContent = aceptarbutton,
            cancelarContent = cancelarButton,
            Links = links.AsList()
        });
        if(owner != null) dialog.Owner = owner;
        return dialog.ShowDialog()!.Value;
    }

    public bool ShowConfirmPassword(string mensaje, string contrasena,string titulo = "Mensaje", UiWindow? owner = null)
    {
        var dialog = new Commons.ConfirmarPassword(new ConfirmPasswordModel()
        {
            Message = mensaje,
            Title = titulo,
            Contrasena = contrasena,
        });
        if (owner != null) dialog.Owner = owner;
        return dialog.ShowDialog()!.Value;
    }
}
