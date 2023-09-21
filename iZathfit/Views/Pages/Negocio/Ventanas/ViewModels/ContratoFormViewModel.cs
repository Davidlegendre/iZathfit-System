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
    public partial class ContratoFormViewModel : ObservableObject
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
        ObservableCollection<PersonaModel>? _personaslist;

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
        string? _subtotal = _nohayplanseleccionadotexto;

        [ObservableProperty]
        string? _descuento = _nohayplanseleccionadotexto;

        [ObservableProperty]
        string? _total = _nohayplanseleccionadotexto;


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

            Personaslist = new ObservableCollection<PersonaModel>(personas);
            Planlist = new ObservableCollection<PlanModel>(planes);
            Tipopagolist = new ObservableCollection<TipoPagoModel>(tipopagos);
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
                Subtotal = _nohayplanseleccionadotexto;
                Descuento = _nohayplanseleccionadotexto;
                Total = _nohayplanseleccionadotexto;
                return;
            }

            FechaInicio = DateTime.Now.AddDays(1).ToLongDateString();
            FechaFin = DateTime.Now.AddMonths(SelectedPlan.MesesTiempo).AddDays(1).ToLongDateString();
            Subtotal = SelectedPlan.GetPrecioString;
            Descuento = SelectedPromo != null ? SelectedPromo.GetDescuento : "no tiene";
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
                FechaFinal = DateTime.Now.AddMonths(SelectedPlan.MesesTiempo).AddDays(1).Date,
                NumeroContrato = CodigoContrato,
                IdTipoPago = SelectedTipoPago.IdtipoPago
            };

            var result = await _helperexcep.ExcepHandler(() => _contratosService.InsertContrato(contrato), win);
            _dialog.ShowDialog(result ? "Contrato guardado correctamente" : "Contrato rechazado", owner: win);
            return result;
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
            return true;
        }
    }
}
