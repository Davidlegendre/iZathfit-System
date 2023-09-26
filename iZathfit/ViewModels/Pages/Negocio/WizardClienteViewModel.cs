using Configuration.GlobalHelpers;
using Dapper;
using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Services.ACliente;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Pages.Negocio
{
    public partial class WizardClienteViewModel : ObservableObject, IDisposable
    {
        IAClienteService? _Service;
        localDialogService? _dialog;
        IExceptionHelperService? _helperexcep;
        IGlobalHelpers? _helpers;
        public WizardClienteViewModel()
        {
            _Service = App.GetService<IAClienteService>();
            _dialog = App.GetService<localDialogService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
            _helpers = App.GetService<IGlobalHelpers>();
        }

        [ObservableProperty]
        string? _Identificacion;

        [ObservableProperty]
        string? _Nombres;

        [ObservableProperty]
        string? _Apellidos;

        [ObservableProperty]
        string? _Direccion = "";

        [ObservableProperty]
        string? _Telefono;

        [ObservableProperty]
        string? _Email ="";

        [ObservableProperty]
        string? _Numemergencia1;

        [ObservableProperty]
        string? _Numemergencia2;

        [ObservableProperty]
        DateTime? _fechNacimiento = DateTime.Now;

        [ObservableProperty]
        ObservableCollection<TipoIdentificacionModel>? _TipoIdentificacionList;

        [ObservableProperty]
        TipoIdentificacionModel? _TipoIdentificacion;

        [ObservableProperty]
        ObservableCollection<GeneroModel>? _GeneroList;

        [ObservableProperty]
        GeneroModel? _GeneroModel;

        [ObservableProperty]
        ObservableCollection<RolModel>? _RolList;

        [ObservableProperty]
        RolModel? _RolModel;

        [ObservableProperty]
        ObservableCollection<Ocupacion>? _OcupacionList;

        [ObservableProperty]
        Ocupacion? _Ocupacionmodel;

        [ObservableProperty]
        ObservableCollection<PadecimientoEnfermedades>? _Padecimientos;

        [ObservableProperty]
        ObservableCollection<PlanModel>? _Planlist;

        [ObservableProperty]
        PlanModel? _SelectedPlan;

        [ObservableProperty]
        bool _EnableIfHavePromociones = false;

        [ObservableProperty]
        ObservableCollection<PromocionModelo>? _Promocioneslist;

        [ObservableProperty]
        PromocionModelo? _SelectedPromo;

        [ObservableProperty]
        ObservableCollection<TipoPagoModel>? _Tipopagolist;

        [ObservableProperty]
        TipoPagoModel? _SelectedTipoPago;

        [ObservableProperty]
        string? _Cantidadpago;

        [ObservableProperty]
        string? _CodigoContrato;

        [ObservableProperty]
        DateTime _FechFin = DateTime.Now;

        [ObservableProperty]
        string? _TitlePromociones = "No Hay Promociones";

        [ObservableProperty]
        DateTime _FechaInicio = DateTime.Now;

        [ObservableProperty]
        string? _FechaFin = "Plan no Seleccionado";

        [ObservableProperty]
        string? _Subtotal = "0.00 S/";

        [ObservableProperty]
        string? _Descuento = "0 %";

        [ObservableProperty]
        string? _Total = "0.00 S/";

        public async Task<bool> InsertACliente(UiWindow win) {
            if (_Service == null || _dialog == null || _helperexcep == null || _helpers == null) return false;
            if(!validar(win)) return false;
            if (_dialog.ShowDialog("Desea Guardar esta Transaccion?", ShowCancelButton: true, owner: win) == false) return false;
            var persona = new PersonaModel()
            {
                Nombres = Nombres,
                Apellidos = Apellidos,
                Fech_Nacimiento = FechNacimiento.Value.Date,
                Direccion = Direccion,
                Telefono = Telefono,
                Email = Email,
                NumeroEmergencia1 = Numemergencia1,
                NumeroEmergencia2 = Numemergencia2,
                IdGenero = GeneroModel.IdGenero,
                Identificacion = Identificacion,
                IdTipoIdentity = TipoIdentificacion.IdTipoIdentity,
                IdRol = RolModel.IdRol,
                IdOcupacion = Ocupacionmodel != null ? Ocupacionmodel.IdOcupacion : OcupacionList.FirstOrDefault().IdOcupacion,
            };

            var contrato = new ContratoModel()
            {
                IdPlan = SelectedPlan.IdPlanes,
                ValorTotal = SelectedPromo != null ? SelectedPromo.GetTotalDescuento : SelectedPlan.Precio,
                Descuento = SelectedPromo != null ? SelectedPromo.DescuentoPercent : 0,
                ValorOriginal = SelectedPlan.Precio,
                FechaFinal = FechFin.Date,
                FechaInicio = FechaInicio.Date,
                NumeroContrato = CodigoContrato,
                FechaFinalReal = DateTime.Parse(FechaFin.Split(": ")[1]),
                IdTipoPago = SelectedTipoPago.IdtipoPago
            };

            var pago = new SaldoXPersonaModel() {
                TotalPagadoActual = Decimal.Parse(Cantidadpago)
            };

            var result = await _helperexcep.ExcepHandler(() => _Service.InsertACliente(persona, pago, 
                Padecimientos != null? Padecimientos.AsList() : new List<PadecimientoEnfermedades>(), 
                contrato), win);

            _dialog.ShowDialog(result ? "Cliente y Sus datos Han sido guardados" : "Transaccion rechazada", owner: win);

            return result;

            /*
              @Nombres = persona.Nombres,
                        @Apellidos = persona.Apellidos,
                        @Fech_Nacimiento = persona.Fech_Nacimiento,
                        @Direccion = persona.Direccion,
                        @Telefono = persona.Telefono,
                        @Email = persona.Email,
                        @NumeroEmergencia1 = persona.NumeroEmergencia1,
                        @NumeroEmergencia2 = persona.NumeroEmergencia2,
                        @idGenero = persona.IdGenero,
                        @Identificacion = persona.Identificacion,
                        @idtipoIdentificacion = persona.IdTipoIdentity,
                        @idRol = persona.IdRol,
                        @idOcupacion = persona.IdOcupacion,

            @IdPlan = contrato.IdPlan,
                        @ValorTotal = contrato.ValorTotal,
                        @Descuento = contrato.Descuento,
                        @ValorOriginal = contrato.ValorOriginal,
                        @FechaFinal = contrato.FechaFinal,
                        @FechaFinalReal = contrato.FechaFinalReal,
                        @NumeroContrato = contrato.NumeroContrato,
                        @IdTipoPago = contrato.IdTipoPago,

                        @TotalPagadoActual = pago.TotalPagadoActual,
             */
        }

        public void Cargarpromociones(List<PromocionModelo>? promociones)
        {
            if (promociones == null || SelectedPlan == null) return;
            var promos = promociones.Where(x => x.IdPlan == SelectedPlan.IdPlanes);
            if (promos.Count() == 0)
            {
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

        public void cargarDatosCalculados()
        {
            if (SelectedPlan == null)
            {
                FechaInicio = DateTime.Now;
                FechaFin = "Plan no Seleccionado";
                Subtotal = "0.00 S/";
                Descuento = "0 %";
                Total = "0.00 S/";
                FechFin = DateTime.Now;
                return;
            }

            FechFin = FechaInicio.AddMonths(SelectedPlan.MesesTiempo);
           
            FechaFin = "Fecha Calculada Final: " + FechFin.ToLongDateString();

            Subtotal = SelectedPlan.GetPrecioString;
            Descuento = SelectedPromo != null ? SelectedPromo.GetDescuento : "0 %";
            Total = SelectedPromo != null ? SelectedPromo.GetTotalEnDescuento : Subtotal;
        }

        public void IngresarPadecimiento(string? padecimiento, UiWindow win) {
            if (string.IsNullOrWhiteSpace(padecimiento))
            {
                _dialog?.ShowDialog("Ingrese un Padecimiento o Enfermedad", owner: win);
                return;
            }
            if (Padecimientos == null)
                Padecimientos = new ObservableCollection<PadecimientoEnfermedades>();
            Padecimientos.Add(new PadecimientoEnfermedades() { 
                Padecimiento = padecimiento
            });
        }

        public void DeletePadecimiento(PadecimientoEnfermedades? padecimiento, UiWindow win) {
            if (padecimiento == null)
            {
                _dialog?.ShowDialog("Se Necesita un Padecimiento o Enfermedad", owner: win);
                return;
            }

            Padecimientos?.Remove(padecimiento);
        }

        bool validar(UiWindow win) {
            if (TipoIdentificacion == null)
            {
                _dialog?.ShowDialog("Seleccione un Tipo de Identificacion", owner: win);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Identificacion))
            {
                _dialog?.ShowDialog("Ingrese una Identificacion", owner: win);
                return false;
            }
            if (!_helpers.IsNullOrWhiteSpaceAndNumber(Identificacion) && TipoIdentificacion.abreviado?.ToUpper() == "DNI")
            {
                _dialog?.ShowDialog("Ingrese una Identificacion valida", owner: win);
                return false;
            }
           

            if (string.IsNullOrWhiteSpace(Nombres))
            {
                _dialog?.ShowDialog("Ingrese los Nombres", owner: win);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Apellidos))
            {
                _dialog?.ShowDialog("Ingrese los Apellidos", owner: win);
                return false;
            }
            if (!_helpers.IsNullOrWhiteSpaceAndNumber(Telefono))
            {
                _dialog?.ShowDialog("Ingrese un Telefono", owner: win);
                return false;
            }
            if (!_helpers.IsNullOrWhiteSpaceAndEmail(Email, true))
            {
                _dialog?.ShowDialog("Ingrese un Email Correcto", owner: win);
                return false;
            }
            if (!_helpers.IsNullOrWhiteSpaceAndNumber(Numemergencia1, true))
            {
                _dialog?.ShowDialog("Numero de Emergencias 1 tiene que ser numero", owner: win);
                return false;
            }
            if (!_helpers.IsNullOrWhiteSpaceAndNumber(Numemergencia2, true))
            {
                _dialog?.ShowDialog("Numemergencia 2 tiene que ser numero", owner: win);
                return false;
            }
            if (FechNacimiento == null)
            {
                _dialog?.ShowDialog("Seleccione la Fecha de Nacimiento", owner: win);
                return false;
            }
            
            if (GeneroModel == null)
            {
                _dialog?.ShowDialog("Seleccione un Genero", owner: win);
                return false;
            }
            if (RolModel == null)
            {
                _dialog?.ShowDialog("Seleccione un Rol", owner: win);
                return false;
            }
            if (TipoIdentificacion == null)
            {
                _dialog?.ShowDialog("Seleccione un Tipo de Identificacion", owner: win);
                return false;
            }
            if (SelectedPlan == null)
            {
                _dialog?.ShowDialog("Seleccione un Plan en Contratos", owner: win);
                return false;
            }
            if (SelectedTipoPago == null)
            {
                _dialog?.ShowDialog("Seleccione un Tipo de Pago", owner: win);
                return false;
            }
            if (!_helpers.IsNullOrWhiteSpaceAndNumber(CodigoContrato))
            {
                _dialog?.ShowDialog("Ingrese el Codigo del Contrato Fisico", owner: win);
                return false;
            }
            if (!_helpers.IsNullOrWhiteSpaceAndDecimal(Cantidadpago))
            {
                _dialog?.ShowDialog("Ingrese la cantidad de Pago", owner: win);
                return false;
            }
            if (FechaInicio.Date < DateTime.Now.Date)
            {
                _dialog?.ShowDialog("Fecha de Inicio no puede ser menor a hoy", owner: win);
                return false;
            }
            if (FechFin.Date < DateTime.Parse(FechaFin.Split(": ")[1]).Date)
            {
                _dialog?.ShowDialog("La fecha de fin no puede ser menor a la fecha final original", owner: win);
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            _Service = null;
            _dialog = null;
            _helperexcep = null;
            _helpers = null;
            Identificacion = null;
            Nombres = null;
            Apellidos = null;Direccion = null;
            Telefono = null;
            Email = null;
            Numemergencia1 = null;
            Numemergencia2 = null;
            FechNacimiento = null;
            TipoIdentificacionList = null;
            TipoIdentificacion = null;
            GeneroList = null;
            GeneroModel = null;
            RolList = null;
            RolModel = null;
            OcupacionList = null;
            Ocupacionmodel = null;
            Padecimientos = null;
            Planlist = null;
            SelectedPlan= null;Promocioneslist = null;
            SelectedPromo = null;
            Tipopagolist = null;
            SelectedTipoPago = null;
            Cantidadpago = null;
            CodigoContrato = null;
            TitlePromociones = null;
            FechaFin = null;
            Descuento = null;
            Total = null;
            Subtotal = null;
        }
    }
}
