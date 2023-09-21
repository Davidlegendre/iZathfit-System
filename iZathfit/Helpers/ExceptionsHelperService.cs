using Configuration.GlobalHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Wpf.Ui.Controls;

namespace iZathfit.Helpers
{
    /// <summary>
    /// IExceptionHelperService: obtiene las excepciones cualquiera y devuelve el resultado
    /// Null si fue error o False si fue error depende
    /// </summary>
    public class ExceptionsHelperService : IExceptionHelperService
    {
        localDialogService _dialog;
        IGlobalHelpers _globalHelpers;
        public ExceptionsHelperService(localDialogService dialog, IGlobalHelpers globalHelpers)
        {
            _dialog = dialog;
            _globalHelpers = globalHelpers;
        }

        public async Task<T?> ExcepHandler<T>(Func<Task<T>> accion, UiWindow owner)
        {
            try
            {
                return await accion();
            }
            catch (Exception ex)
            {
                _dialog.ShowDialog(mensaje: "No se alarme, solo contacte a algun desarrollador en caso de dudas\n\nError: " + ex.Message,
                    titulo: "Ups",
                    links: _globalHelpers.GetLinksContacts(), owner: owner);
                return default;
            }
        }
        public async Task<bool> ExcepHandler(Func<Task> accion, UiWindow owner)
        {
            try
            {
                await accion();
                return true;
            }
            catch (Exception ex)
            {
                _dialog.ShowDialog(mensaje: "No se alarme, solo contacte a algun desarrollador en caso de dudas\n\nError: " + ex.Message,
                    titulo: "Ups",
                    links: _globalHelpers.GetLinksContacts(), owner: owner);
                return false;
            }
        }
    }
}
