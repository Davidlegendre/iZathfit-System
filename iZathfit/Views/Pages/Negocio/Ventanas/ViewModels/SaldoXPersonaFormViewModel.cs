using iZathfit.Helpers;
using Models;
using Services.Contratos;
using Services.Persona;
using Services.SaldoXPersona;
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
    public partial class SaldoXPersonaFormViewModel : ObservableObject
    {
        ITipoPagoService? _tipopagoService;
        IContratosService? _contratoService;
        IPersonaService? _personaService;
        localDialogService? _dialog;
        IExceptionHelperService? _helperexcep;
        ISaldoXPersonaService? _saldoXPersonaService;
        public SaldoXPersonaFormViewModel()
        {
            _tipopagoService = App.GetService<ITipoPagoService>();
            _contratoService = App.GetService<IContratosService>();
            _personaService = App.GetService<IPersonaService>();
            _dialog = App.GetService<localDialogService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
            _saldoXPersonaService = App.GetService<ISaldoXPersonaService>();
        }

        [ObservableProperty]
        ObservableCollection<TipoPagoModel>? _tiposdepagolist;

        [ObservableProperty]
        ObservableCollection<ContratoModel>? _contratolistByperson;

        [ObservableProperty]
        TipoPagoModel? _tipopagoselected;

        [ObservableProperty]
        PersonaModel? _personaselected;

        [ObservableProperty]
        ContratoModel? _contratoselected;

        [ObservableProperty]
        string? _cantidadpago = "";



        public async Task<bool> Getdata(UiWindow win) { 
            if(_tipopagoService == null || _contratoService == null || _personaService == null || _dialog == null || _helperexcep == null)
                return false;
            var tipopagos = await _helperexcep.ExcepHandler(() => _tipopagoService.GetTipoPagos(), win);
            var contratos = await _helperexcep.ExcepHandler(()=> _contratoService.GetContratos(), win);
            var personas = await _helperexcep.ExcepHandler(()=>_personaService.SelectAllAll(), win);
            if (tipopagos == null || contratos == null || personas == null)
            {
                _dialog.ShowDialog("No podemos continuar debido a que faltan: tipo de pagos, contratos o personas", owner: win);
                return false;
            }

            Tiposdepagolist = new ObservableCollection<TipoPagoModel>(tipopagos);

            return true;

        }

        public async Task GetContratosByPerson(Guid idperson, UiWindow win)
        {
            if ( _contratoService == null || _dialog == null || _helperexcep == null)
                return;
            
            var result = await _helperexcep.ExcepHandler(() => _contratoService.SearchContratoByPersona(idperson), win);
            ContratolistByperson = result == null ? null
                 : new ObservableCollection<ContratoModel>(result);


        }

        public async Task<bool> Guardar(UiWindow win) {
            if (_tipopagoService == null || _contratoService == null || _saldoXPersonaService == null 
                || _personaService == null || _dialog == null || _helperexcep == null)
                return false;
            if(!validar(win)) return false;
            if (_dialog?.ShowDialog("Desea Guardar este Pago?", ShowCancelButton: true, owner: win) == false) return false;

            var saldoxpersona = new SaldoXPersonaModel() {
                IdPersona = Personaselected.IdPersona,
                IdContrato = Contratoselected.IdContrato,
                TotalPagadoActual = Decimal.Parse(Cantidadpago, new CultureInfo("es-PE")),
                IdTipoPago = Tipopagoselected.IdtipoPago
            };

            var result = await _helperexcep.ExcepHandler(() => _saldoXPersonaService.InsertSaldoXPersona(saldoxpersona), win);
            _dialog?.ShowDialog(result ? "Pago Guardado" : "Pago Rechazado", owner: win);
            return result;
        }

        
        bool validar(UiWindow win)
        {
            if (Personaselected == null)
            {
                _dialog?.ShowDialog("Busque a una persona", owner: win);
                return false;
            }
            if(Contratoselected == null)
            {
                _dialog?.ShowDialog("Seleccione un Contrato", owner: win);
                return false;
            }
            if (Tipopagoselected == null)
            {
                _dialog?.ShowDialog("Seleccione un tipo de pago", owner: win);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Cantidadpago))
            {
                _dialog?.ShowDialog("Escriba una cantidad de pago", owner: win);
                return false;
            }
            return true;
        }
    }
}
