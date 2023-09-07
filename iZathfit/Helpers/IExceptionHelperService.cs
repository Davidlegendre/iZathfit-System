using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Helpers
{
    public interface IExceptionHelperService
    {
        public Task<T?> ExcepHandler<T>(Func<Task<T>> accion, UiWindow owner);
    }
}
