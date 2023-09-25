using iZathfit.Helpers;
using Models;
using Services.TipoPago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels
{
    public partial class MTipoPagoViewModel : ObservableObject, IDisposable
    {
        ITipoPagoService? _servicio;
        localDialogService? _dialog;
        IExceptionHelperService? _helperexcep;
        public MTipoPagoViewModel()
        {
            _servicio = App.GetService<ITipoPagoService>();
            _dialog = App.GetService<localDialogService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
        }

        [ObservableProperty]
        string _tipoPago = "";

        [ObservableProperty]
        Visibility _visibleLimpiarButton = Visibility.Visible;

        [ObservableProperty]
        string _Titulo = "";

        public void Cargar(TipoPagoModel? model = null)
        {
            Titulo = model != null ? "Modificar" : "Agregar";
            VisibleLimpiarButton = model == null ? Visibility.Visible : Visibility.Collapsed;

            if (model != null)
            {
                TipoPago = model.descripcion;
            }

        }

        public async Task<bool> Guardar(UiWindow win)
        {
            if (_servicio == null || _dialog == null || _helperexcep == null) return false;
            if (!validar()) return false;
            if (!_dialog.ShowDialog("Desea guardar este Tipo de pago?", ShowCancelButton: true, owner: win)) return false;
            var result = await _helperexcep.ExcepHandler(() => _servicio.InsertTipoPago(new TipoPagoModel() { descripcion = TipoPago }), win);
            return result;
        }

        public async Task<bool> Modificar(UiWindow win, TipoPagoModel tipopago)
        {
            if (_servicio == null || _dialog == null || _helperexcep == null) return false;
            if (!validar()) return false;
            if (!_dialog.ShowDialog("Desea modificar este Tipo de pago?", ShowCancelButton: true, owner: win)) return false;
            var result = await _helperexcep.ExcepHandler(() => _servicio.UpdateTipoPago(
                new TipoPagoModel() { descripcion = TipoPago, IdtipoPago = tipopago.IdtipoPago }), win);
            return result;
        }

        bool validar()
        {
            if (string.IsNullOrWhiteSpace(TipoPago))
            {
                _dialog?.ShowDialog("Tipo de Pago esta vacio");
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            _servicio = null;
            _dialog = null;
            _helperexcep = null;
           
        }
    }
}
