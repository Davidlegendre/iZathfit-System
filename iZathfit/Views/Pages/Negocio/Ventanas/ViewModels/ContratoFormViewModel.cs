using Configuration.GlobalHelpers;
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
        IGlobalHelpers? _helpers;
        public ContratoFormViewModel()
        {
            _personaService = App.GetService<IPersonaService>();
            _planService = App.GetService<IPlanService>();
            _tipoPagoService = App.GetService<ITipoPagoService>();
            _promocionService = App.GetService<IPromocionService>();
            _dialog = App.GetService<localDialogService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
            _contratosService = App.GetService<IContratosService>();
            _helpers = App.GetService<IGlobalHelpers>();
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
        DateTime _fechaInicio = DateTime.Now;

        [ObservableProperty]
        string? _fechaFin = _nohayplanseleccionadotexto;

        [ObservableProperty]
        DateTime _dateselected = DateTime.Now;

        [ObservableProperty]
        string? _total;

       


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
                FechaInicio = DateTime.Now;
                FechaFin = _nohayplanseleccionadotexto;
                Total = "";
                Dateselected = DateTime.Now;
                return;
            }

            Dateselected = FechaInicio.AddMonths(SelectedPlan.MesesTiempo);
           
            FechaFin = "Fecha Calculada Final: " +  Dateselected.ToLongDateString();

            Total = SelectedPromo != null? SelectedPromo.PromoPrecio.ToString("0.00") : SelectedPlan.Precio.ToString("0.00");         
        }

        public async Task<bool> Guardar(UiWindow win)
        {
            if (_helperexcep == null || _contratosService == null || _dialog == null) return false;
            if (!validar(win)) return false;
            if(_dialog?.ShowDialog("Desea agregar este contrato?", ShowCancelButton: true, owner: win) == false) return false;

            var contrato = new ContratoModel() {
                IdPlan = SelectedPlan.IdPlanes,
                IdPersona = SelectedPersona.IdPersona,
                ValorTotal = Decimal.Parse(Total),
                ValorOriginal = SelectedPlan.Precio,
                FechaInicio = FechaInicio,
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

            var contrato = new ContratoModel()
            {
                IdContrato = idcontrato,
                FechaFinal = Dateselected.Date,
                NumeroContrato = CodigoContrato,
                FechaInicio = FechaInicio,
                FechaFinalReal = DateTime.Parse(FechaFin.Split(": ")[1])
            };

            var result = await _helperexcep.ExcepHandler(() => _contratosService.UpdateContrato(contrato), win);
            _dialog.ShowDialog(result ? "Contrato modificado correctamente" : "Contrato rechazado", owner: win);
            return result;

        }


        bool validarUpdate(UiWindow win) {
            if (_helpers.IsNullOrWhiteSpaceAndNumber(CodigoContrato))
            {
                _dialog?.ShowDialog("Codigo de contrato fisico no esta ingresado o es incorrecto", owner: win);
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
            if (!_helpers.IsNullOrWhiteSpaceAndDecimal(Total))
            {
                _dialog?.ShowDialog("Total a Pagar esta incorrecto", owner: win);
                return false;
            }
            if (!_helpers.IsNullOrWhiteSpaceAndNumber(CodigoContrato))
            {
                _dialog?.ShowDialog("Codigo de contrato fisico no esta ingresado o es incorrecto", owner: win);
                return false;
            }
            if (FechaInicio.Date < DateTime.Now.Date)
            {
                _dialog?.ShowDialog("Fecha de Inicio no puede ser menor a hoy", owner: win);
                return false;
            }
            if (Dateselected.Date == DateTime.Now.Date)
            {
                _dialog?.ShowDialog("Selecione una fecha final", owner: win);
                return false;
            }
            if (Dateselected.Date < FechaInicio.AddMonths(SelectedPlan.MesesTiempo).Date)
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
