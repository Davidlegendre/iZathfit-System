using Services.PadecimientosEnfermedades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iZathfit.ViewModels.Pages
{
    public partial class MantenimientoNumeroEmergenciasVM : ObservableObject
    {
        IPadecimientosEnfermedadesService? _servicio;
        localDialogService? _dialog;
    }
}
