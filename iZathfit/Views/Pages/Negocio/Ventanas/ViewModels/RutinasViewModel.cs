using Configuration.GlobalHelpers;
using iZathfit.Helpers;
using Models;
using Services.Rutina;
using Services.TipoPago;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Negocio.Ventanas.ViewModels
{
    public partial class RutinasViewModel :ObservableObject, IDisposable
    {
        IRutinaService? _service;
        ITipoPagoService? _tipoPagoService;
        IExceptionHelperService? _helpexec;
        localDialogService? _dialog;
        IGlobalHelpers? _helpers;
        public RutinasViewModel()
        {
            _service =App.GetService<IRutinaService>();
            _tipoPagoService = App.GetService<ITipoPagoService>();
            _helpexec = App.GetService<IExceptionHelperService>();
            _dialog = App.GetService<localDialogService>();
            _helpers=App.GetService<IGlobalHelpers>();
        }

        [ObservableProperty]
        ObservableCollection<TipoPagoModel>? _tipopagolist;

        [ObservableProperty]
        string? _monto = "";

        [ObservableProperty]
        TipoPagoModel? _tipopagoselect;

        public async Task<bool> GetData(UiWindow win, RutinaModel? model = null)
        {
            if (_tipoPagoService == null || _helpexec == null || _dialog ==null) return false;
            var result = await _helpexec.ExcepHandler(() => _tipoPagoService.GetTipoPagos(), win);
            if (result == null) {

                _dialog.ShowDialog("No podemos continuar ya que no hay tipo de pagos", owner: win);
                return false;
            }

            Tipopagolist = new ObservableCollection<TipoPagoModel>(result);

            if(model != null)
            {
                Monto = model.MontoPago.ToString("0.00");
                Tipopagoselect = Tipopagolist.First(x => x.IdtipoPago == model.IdTipoPago);
            }

            return true;
        }

        public async Task<bool> InsertRutina(UiWindow win) {
            if (_tipoPagoService == null || _helpexec == null || _dialog == null || _service == null) return false;
            if(!validar(win)) return false;
            if(_dialog.ShowDialog("Desea Ingresar esta Rutina con monto: " + Decimal.Parse(Monto).ToString("0.00") + " S/?",
                ShowCancelButton: true,owner: win) == false) return false;

            var result = await _helpexec.ExcepHandler(() => _service.InsertarRutina(new() { 
                IdTipoPago = Tipopagoselect.IdtipoPago,
                MontoPago = Decimal.Parse(Monto)
            }), win);

            _dialog.ShowDialog(result ? "Rutina Ingresada Correctamente" : "Rutina Rechazada", owner: win);

            return result;
        }

        public async Task<bool> UpdateRutina(UiWindow win, Guid idrutina)
        {
            if (_tipoPagoService == null || _helpexec == null || _dialog == null || _service == null) return false;
            if (!validar(win)) return false;
            if (_dialog.ShowDialog("Desea Modificar esta Rutina con monto: " + Decimal.Parse(Monto).ToString("0.00") + " S/?",
                ShowCancelButton: true, owner: win) == false) return false;

            var result = await _helpexec.ExcepHandler(() => _service.UpdateRutina(new()
            {
                IdRutina = idrutina,
                IdTipoPago = Tipopagoselect.IdtipoPago,
                MontoPago = Decimal.Parse(Monto)
            }), win);

            _dialog.ShowDialog(result ? "Rutina Modificada Correctamente" : "Rutina Rechazada", owner: win);

            return result;
        }

        bool validar(UiWindow win)
        {
            if (Tipopagoselect == null)
            {
                _dialog?.ShowDialog("Seleccion un tipo de pago", owner: win);
                return false;
            }
            if (!_helpers.IsNullOrWhiteSpaceAndDecimal(Monto))
            {
                _dialog?.ShowDialog("Monto esta vacio", owner: win);
                return false;   
            }

            return true;
        }

        public void Dispose()
        {
            _service = null;
            _tipoPagoService = null;
            _helpexec = null;
            _dialog = null;
            Tipopagolist = null;
            Tipopagoselect = null;

        }
    }
}
