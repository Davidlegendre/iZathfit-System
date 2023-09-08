using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iZathfit.ViewModels.Pages
{
    public partial class MantenimientoPersonasVM : ObservableObject
    {
        public MantenimientoPersonasVM()
        {
            Progreso = 0;
        }

        [ObservableProperty]
        int progreso;

        [RelayCommand]
        void cambiarprogreso() {
            Progreso = 10;
        }
    }
}
