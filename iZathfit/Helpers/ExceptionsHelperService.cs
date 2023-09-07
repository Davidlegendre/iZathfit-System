using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Wpf.Ui.Controls;

namespace iZathfit.Helpers
{
    public class ExceptionsHelperService : IExceptionHelperService
    {
        localDialogService _dialog;
        public ExceptionsHelperService(localDialogService dialog)
        {
            _dialog = dialog;
        }

        public async Task<T?> ExcepHandler<T>(Func<Task<T>> accion, UiWindow owner)
        {
            try
            {
                return await accion();
            }
            catch (Exception ex)
            {
                _dialog.ShowDialog(new Models.ModelsCommons.DialogModel()
                {
                    Title = "Un Error a Ocurrido",
                    Message = "No se alarme, solo contacte al desarrollador en caso de dudas\n" + ex.Message,
                    Links = new List<Models.ModelsCommons.LinkModel>() {
                         new Models.ModelsCommons.LinkModel(){ TitlePage = "Whatsapp del Desarrollador", Url = "https://www.google.com"}
                    },
                    canShowCancelButton = false
                }, owner);
                return default;
            }
        }
    }
}
