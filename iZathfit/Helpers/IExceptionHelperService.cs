using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Helpers
{
    /// <summary>
    /// IExceptionHelperService: obtiene las excepciones cualquiera y devuelve el resultado
    /// Null si fue error o False si fue error depende
    /// </summary>
    public interface IExceptionHelperService
    {
        
        public Task<T?> ExcepHandler<T>(Func<Task<T>> accion, UiWindow owner, bool ShowMsn = true);
        public Task<bool> ExcepHandler(Func<Task> accion, UiWindow owner, bool ShowMsn = true);
    }
}
