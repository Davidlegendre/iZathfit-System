using Dapper;
using iZathfit.ViewModels.Pages.Negocio;
using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios;
using iZathfit.Views.Windows;
using Models;
using Services.Genero;
using Services.Ocupacion;
using Services.Plan;
using Services.Promocion;
using Services.Rol;
using Services.TipoIdentificacion;
using Services.TipoPago;
using Stfu.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Negocio
{
    /// <summary>
    /// Lógica de interacción para WizardCliente.xaml
    /// </summary>
    public partial class WizardCliente : UiWindow, IDisposable
    {
        WizardClienteViewModel? _vm;
        ITipoIdentificacionService? _tipoIdentifyService;
        IGeneroService? _generoService;
        IRolService? _rolService;
        IOcupacionService? _ocupacionService;
        IPlanService? _planService;
        ITipoPagoService? _tipoPagoService;
        IPromocionService? _promocionService;
        public WizardCliente()
        {
            InitializeComponent();
            _vm = DataContext as WizardClienteViewModel;
            _tipoIdentifyService = App.GetService<ITipoIdentificacionService>();
            _generoService = App.GetService<IGeneroService>();
            _rolService = App.GetService<IRolService>();
            _ocupacionService = App.GetService<IOcupacionService>();
            _planService = App.GetService<IPlanService>();
            _tipoPagoService = App.GetService<ITipoPagoService>();
            _promocionService = App.GetService<IPromocionService>();
            this.Loaded += WizardCliente_Loaded;
            this.Closing += WizardCliente_Closing;
        }

        private void WizardCliente_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispose();

        }

        List<PromocionModelo>? _PromosActivos;

        private async void WizardCliente_Loaded(object sender, RoutedEventArgs e)
        {
            if(_vm != null && _tipoIdentifyService != null && _generoService != null 
                && _rolService != null && _ocupacionService != null && _planService != null && _tipoPagoService!= null&& _promocionService !=null)
            {
                var tipoidents = await _tipoIdentifyService.GetTipoIdentificaciones();
                var generos = await _generoService.GetGeneros();
                var roles = await _rolService.GetRoles();
                var ocupaciones = await _ocupacionService.GetOcupaciones();
                var planservice = await _planService.GetPlanes();
                var tipopagos = await _tipoPagoService.GetTipoPagos();
                var promos = await _promocionService.GetPromocionesActive();
                _PromosActivos = promos;
                datepicker.DateSelect = DateTime.Now;
                dtFechaFin.DateSelect = DateTime.Now;
                fechaINICIO.DateSelect = DateTime.Now;
                
                _vm.TipoIdentificacionList = tipoidents != null ? new ObservableCollection<Models.TipoIdentificacionModel>(tipoidents) : null;
                _vm.GeneroList = generos != null ? new ObservableCollection<Models.GeneroModel>(generos) : null;
                _vm.RolList = roles != null ? new ObservableCollection<Models.RolModel>(roles) : null;
                _vm.OcupacionList = ocupaciones != null ? new ObservableCollection<Models.Ocupacion>(ocupaciones) : null;
                _vm.Planlist = planservice != null ? new ObservableCollection<PlanModel>(planservice) : null;
                _vm.Tipopagolist = tipopagos != null ? new ObservableCollection<TipoPagoModel>(tipopagos) : null;
                _vm.RolModel = roles?.Count() == 1? roles?.FirstOrDefault() : roles?.FirstOrDefault(x => x.Code == "CLNT");
                _vm.Ocupacionmodel = ocupaciones?.FirstOrDefault();
                


                tipoidents = null; generos = null; roles = null; ocupaciones = null; planservice = null;
                tipopagos = null;
            }
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAtras_click(object sender, RoutedEventArgs e)
        {
            if(tabpages.SelectedIndex != 0)
            {
                tabpages.SelectedIndex -= 1;
            }
        }

        private void btnSiguiente_click(object sender, RoutedEventArgs e)
        {
            if (tabpages.SelectedIndex < tabpages.Items.Count)
            {
                tabpages.SelectedIndex += 1;
            }
        }

        private void datepicker_DateSelectChanged(object sender, DateTime e)
        {
            if(_vm != null)
            {
                _vm.FechNacimiento = e;
            }
        }

        private void dtFechaFin_DateSelectChanged(object sender, DateTime e)
        {
            if (_vm != null)
            {
                _vm.FechFin = e;
            }
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                if (await _vm.InsertACliente(this) == true)
                {
                    Close();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                _vm.IngresarPadecimiento(txtpadecimiento.Text, this);
            }
        }

        private void txtpadecimiento_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && _vm != null)
            {
                _vm.IngresarPadecimiento(txtpadecimiento.Text, this);
                txtpadecimiento.Clear();
            }
        }

        private void btneliminar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Wpf.Ui.Controls.Button;
            var context = button?.DataContext as PadecimientoEnfermedades;
            if (context != null && _vm != null)
            {
                _vm.DeletePadecimiento(context, this);
                txtpadecimiento.Clear();
                txtpadecimiento.Focus();
            }
        }


        private void planSelect_Change(object sender, SelectionChangedEventArgs e)
        {
            if (_vm == null) return;
            fechaINICIO.DateSelect = DateTime.Now;
            _vm.Cargarpromociones(_PromosActivos);
            _vm.cargarDatosCalculados();
            dtFechaFin.DateSelect = _vm.FechFin;
        }

        private void btnQuitarPromoSelected_click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            _vm.SelectedPromo = null;
        }

        private void promos_selectedchange(object sender, SelectionChangedEventArgs e)
        {
            if (_vm == null) return;
            _vm.cargarDatosCalculados();
        }

        public void Dispose()
        {
            _vm?.Dispose();
            _tipoIdentifyService = null;
            _generoService = null;
            _rolService = null;
            _ocupacionService = null;
            _planService = null;
            _tipoPagoService = null;
            _promocionService = null;
            _PromosActivos = null;
            App.GetService<MainWindow>()?.Alzeimer();
        }

        private void btnAddOcupacion_click(object sender, RoutedEventArgs e)
        {
            var win = new MOcupacionesForm(ocupacions: _vm.OcupacionList.AsList(), ocupacionescollection: _vm.OcupacionList);
            win.Owner = this;
            win.ShowDialog();
        }

        private void fechaINICIO_DateSelectChanged(object sender, DateTime e)
        {
            if (_vm == null) return;
            _vm.FechaInicio = e;
            _vm.cargarDatosCalculados();
            dtFechaFin.DateSelect = _vm.FechFin;
        }
    }
}
