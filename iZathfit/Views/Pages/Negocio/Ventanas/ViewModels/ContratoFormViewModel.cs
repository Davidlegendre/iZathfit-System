using iZathfit.Helpers;
using Models;
using Services.Contratos;
using Services.Persona;
using Services.Plan;
using Services.Promocion;
using Services.TipoPago;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Negocio.Ventanas.ViewModels
{
    public partial class ContratoFormViewModel : ObservableObject, IDisposable
    {
        IPersonaService? _personaService;
        IPlanService? _planService;
        ITipoPagoService? _tipoPagoService;
        IPromocionService? _promocionService;
        IContratosService? _contratosService;
        localDialogService? _dialog;
        IExceptionHelperService? _helperexcep;
        public ContratoFormViewModel()
        {
            _personaService = App.GetService<IPersonaService>();
            _planService = App.GetService<IPlanService>();
            _tipoPagoService = App.GetService<ITipoPagoService>();
            _promocionService = App.GetService<IPromocionService>();
            _dialog = App.GetService<localDialogService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
            _contratosService = App.GetService<IContratosService>();
        }

       static string _nohayplanseleccionadotexto = "Plan No seleccionado";

        [ObservableProperty]
        ObservableCollection<PlanModel>? _planlist;

        [ObservableProperty]
        ObservableCollection<TipoPagoModel>? _tipopagolist;

        [ObservableProperty]
        ObservableCollection<PromocionModelo>? _promocioneslist;

        List<PromocionModelo>? _promosoriginal;

        [ObservableProperty]
        bool _enableIfHavePromociones = false;

        [ObservableProperty]
        string? _titlePromociones = "Promociones (No hay para el plan)";

        [ObservableProperty]
        PlanModel? _selectedPlan = null;

        [ObservableProperty]
        TipoPagoModel? _selectedTipoPago = null;

        [ObservableProperty]
        PromocionModelo? _selectedPromo = null;

        [ObservableProperty]
        PersonaModel? _selectedPersona = null;

        [ObservableProperty]
        string? _codigoContrato = "";

        [ObservableProperty]
        string? _fechaInicio = _nohayplanseleccionadotexto;

        [ObservableProperty]
        string? _fechaFin = _nohayplanseleccionadotexto;

        [ObservableProperty]
        DateTime _dateselected = DateTime.Now;

        [ObservableProperty]
        string? _subtotal = "0.00 S/";

        [ObservableProperty]
        string? _descuento ="0 %" ;

        [ObservableProperty]
        string? _total = "0.00 S/";

       


        public async Task<bool> Cargardatos(UiWindow win) {
            if (_personaService == null || _planService == null || _tipoPagoService == null || _helperexcep == null || _dialog == null
                || _promocionService == null) return false;
            var personas = await _helperexcep.ExcepHandler(() => _personaService.SelectAllAll(), win);
            var planes = await _helperexcep.ExcepHandler(() => _planService.GetPlanes(), win);
            var tipopagos = await _helperexcep.ExcepHandler(() => _tipoPagoService.GetTipoPagos(), win);
            var promos = await _helperexcep.ExcepHandler(() => _promocionService.GetPromocionesActive(), win);
            if (personas == null || planes == null || tipopagos == null) {
                _dialog.ShowDialog("No podemos continuar porque faltan datos, revise: personas, planes y tipos de pagos", owner: win);
                return false;
            }

            Planlist = new ObservableCollection<PlanModel>(planes);
            Tipopagolist = new ObservableCollection<TipoPagoModel>(tipopagos);
            FechaFin = _nohayplanseleccionadotexto;

            _promosoriginal = promos;
            return true;
        }

        public void Cargarpromociones() {
            if (_promocionService == null || _promosoriginal == null || SelectedPlan == null) return;
            var promos = _promosoriginal.Where(x => x.IdPlan == SelectedPlan.IdPlanes);
            if (promos.Count() == 0) {
                Promocioneslist = null;
                EnableIfHavePromociones = false;
                TitlePromociones = "Promociones (No hay para el plan)";
                SelectedPromo = null;
                return;
            }
            Promocioneslist = new ObservableCollection<PromocionModelo>(promos);
            EnableIfHavePromociones = true;
            TitlePromociones = "Promociones ¡Hay Promos!";            
        }

        public void cargarDatosCalculados() {
            if (SelectedPlan == null) {
                FechaInicio = _nohayplanseleccionadotexto;
                FechaFin = _nohayplanseleccionadotexto;
                Subtotal = "0.00 S/";
                Descuento = "0 %";
                Total = "0.00 S/";
                Dateselected = DateTime.Now;
                return;
            }
            Dateselected = DateTime.Now.AddMonths(SelectedPlan.MesesTiempo);
            FechaInicio = DateTime.Now.Date.ToLongDateString();
            FechaFin = "Fecha Calculada Final: " +  Dateselected.ToLongDateString();
            
            Subtotal = SelectedPlan.GetPrecioString;
            Descuento = SelectedPromo != null ? SelectedPromo.GetDescuento : "0 %";
            Total = SelectedPromo != null ? SelectedPromo.GetTotalEnDescuento : Subtotal;            
        }

        public async Task<bool> Guardar(UiWindow win)
        {
            if (_helperexcep == null || _contratosService == null || _dialog == null) return false;
            if (!validar(win)) return false;
            if(_dialog?.ShowDialog("Desea agregar este contrato?", ShowCancelButton: true, owner: win) == false) return false;

            var contrato = new ContratoModel() {
                IdPlan = SelectedPlan.IdPlanes,
                IdPersona = SelectedPersona.IdPersona,
                ValorTotal = SelectedPromo != null ? SelectedPromo.GetTotalDescuento : SelectedPlan.Precio,
                Descuento = SelectedPromo != null ? SelectedPromo.DescuentoPercent : 0,
                ValorOriginal = SelectedPlan.Precio,
                FechaFinal = Dateselected.Date,
                NumeroContrato = CodigoContrato,
                FechaFinalReal = DateTime.Parse(FechaFin.Split(": ")[1]),
                IdTipoPago = SelectedTipoPago.IdtipoPago
            };

            var result = await _helperexcep.ExcepHandler(() => _contratosService.InsertContrato(contrato), win);
            _dialog.ShowDialog(result ? "Contrato guardado correctamente" : "Contrato rechazado", owner: win);
            return result;
        }


        public async Task<bool> UpdateContrato(UiWindow win, Guid idcontrato) {
            if (_helperexcep == null || _contratosService == null || _dialog == null) return false;
            if(!validarUpdate(win)) return false;
            if (_dialog?.ShowDialog("Desea modificar este contrato?", ShowCancelButton: true, owner: win) == false) return false;

            var result = await _helperexcep.ExcepHandler(() => _contratosService.UpdateContrato(idcontrato, Dateselected.Date, CodigoContrato), win);
            _dialog.ShowDialog(result ? "Contrato modificado correctamente" : "Contrato rechazado", owner: win);
            return result;

        }


        bool validarUpdate(UiWindow win) {
            if (string.IsNullOrWhiteSpace(CodigoContrato))
            {
                _dialog?.ShowDialog("Codigo de contrato fisico no esta ingresado", owner: win);
                return false;
            }
            if (Dateselected.Date == DateTime.Now.Date)
            {
                _dialog?.ShowDialog("Selecione una fecha final", owner: win);
                return false;
            }
            if (Dateselected.Date < DateTime.Parse(FechaFin.Split(": ")[1]).Date)
            {
                _dialog?.ShowDialog("La fecha final no debe ser menor a la calculada", owner: win);
                return false;
            }
            return true;
        }


        bool validar(UiWindow win) {
            if (SelectedPersona == null)
            {
                _dialog?.ShowDialog("No hay una persona seleccionada", owner: win);
                return false;
            }
            if (SelectedPlan == null)
            {
                _dialog?.ShowDialog("No hay un Plan seleccionado", owner: win);
                return false;
            }
            if (SelectedTipoPago == null)
            {
                _dialog?.ShowDialog("No hay un Tipo de Pago seleccionado", owner: win);
                return false;
            }
            if (string.IsNullOrWhiteSpace(CodigoContrato))
            {
                _dialog?.ShowDialog("Codigo de contrato fisico no esta ingresado", owner: win);
                return false;
            }
            if (CodigoContrato.Length < 6)
            {
                _dialog?.ShowDialog("El Codigo de contrato debe tener 6 digitos", owner: win);
                return false;
            }
            if (Dateselected.Date == DateTime.Now.Date)
            {
                _dialog?.ShowDialog("Selecione una fecha final", owner: win);
                return false;
            }
            if (Dateselected.Date < DateTime.Now.AddMonths(SelectedPlan.MesesTiempo).Date)
            {
                _dialog?.ShowDialog("La fecha final no debe ser menor a la calculada", owner: win);
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            _personaService = null;
            _planService = null;
            _tipoPagoService = null;
            _promocionService = null;
            _dialog = null;
            _helperexcep = null;
            _contratosService =null;
            Planlist = null;
            Tipopagolist = null;
            Promocioneslist = null;
            SelectedPersona= null;
            SelectedPlan = null;
            SelectedPromo = null;
            SelectedTipoPago = null;
        }
    }
}
