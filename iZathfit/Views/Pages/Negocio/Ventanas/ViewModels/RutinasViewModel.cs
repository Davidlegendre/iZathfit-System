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
    public partial class RutinasViewModel :ObservableObject
    {
        IRutinaService? _service;
        ITipoPagoService? _tipoPagoService;
        IExceptionHelperService? _helpexec;
        localDialogService? _dialog;
        public RutinasViewModel()
        {
            _service =App.GetService<IRutinaService>();
            _tipoPagoService = App.GetService<ITipoPagoService>();
            _helpexec = App.GetService<IExceptionHelperService>();
            _dialog = App.GetService<localDialogService>();
        }

        [ObservableProperty]
        ObservableCollection<TipoPagoModel>? _tipopagolist;

        [ObservableProperty]
        string? _monto = "";

        [ObservableProperty]
        TipoPagoModel? _tipopagoselect;

        public async Task<bool> GetData(UiWindow win)
        {
            if (_tipoPagoService == null || _helpexec == null || _dialog ==null) return false;
            var result = await _helpexec.ExcepHandler(() => _tipoPagoService.GetTipoPagos(), win);
            if (result == null) {

                _dialog.ShowDialog("No podemos continuar ya que no hay tipo de pagos", owner: win);
                return false;
            }

            Tipopagolist = new ObservableCollection<TipoPagoModel>(result);

            return true;
        }

        public async Task<bool> InsertRutina(UiWindow win) {
            if (_tipoPagoService == null || _helpexec == null || _dialog == null || _service == null) return false;
            if(!validar(win)) return false;

            var result = await _helpexec.ExcepHandler(() => _service.InsertarRutina(new() { 
                IdTipoPago = Tipopagoselect.IdtipoPago,
                MontoPago = Decimal.Parse(Monto, new CultureInfo("es-PE"))
            }), win);

            _dialog.ShowDialog(result ? "Rutina Ingresada Correctamente" : "Rutina Rechazada", owner: win);

            return result;
        }
        bool validar(UiWindow win)
        {
            if (Tipopagoselect == null)
            {
                _dialog?.ShowDialog("Seleccion un tipo de pago", owner: win);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Monto))
            {
                _dialog?.ShowDialog("Monto esta vacio", owner: win);
                return false;   
            }

            return true;
        }
    }
}
