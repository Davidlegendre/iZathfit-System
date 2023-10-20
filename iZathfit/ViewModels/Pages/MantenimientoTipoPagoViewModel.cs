using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Services.TipoPago;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Pages
{
    public partial class MantenimientoTipoPagoViewModel : ObservableObject, IDisposable
    {
        ITipoPagoService? _servicio;
        IExceptionHelperService? _helperexcep;
        localDialogService? _dialog;

        public MantenimientoTipoPagoViewModel()
        {
            _servicio = App.GetService<ITipoPagoService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
            _dialog = App.GetService<localDialogService>();
        }

        [ObservableProperty]
        ObservableCollection<TipoPagoModel>? _tipopagoslist;

        [ObservableProperty]
        int _Columns = 4;

        public async Task CargarDatos() {
            if (_servicio == null || _helperexcep == null) return;
            var result = await _helperexcep.ExcepHandler(() => _servicio.GetTipoPagos(), App.GetService<MainWindow>());
            Tipopagoslist = result != null ? new ObservableCollection<TipoPagoModel>(result) : null;

        }

        public async Task Delete(int idtipopago) {
            if (_servicio == null || _helperexcep == null) return;
            var result = await _helperexcep.ExcepHandler(() => _servicio.DeleteTipoPago(idtipopago), App.GetService<MainWindow>());

        
            if (result == false)
            {
                _dialog?.ShowDialog("No se puede eliminar el tipo de Pago", owner: App.GetService<MainWindow>());
                return;
            }

            _dialog?.ShowDialog("Tipo de Pago Eliminada Correctamente", owner: App.GetService<MainWindow>());

            Tipopagoslist.Remove(Tipopagoslist.First(x => x.IdtipoPago == idtipopago));
        }

        public void Dispose()
        {
            Tipopagoslist = null;
        }
    }
}
