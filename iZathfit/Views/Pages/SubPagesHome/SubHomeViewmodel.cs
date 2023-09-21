using Dapper;
using iZathfit.Views.Windows;
using Models;
using Models.DTOS;
using Models.ModelsCommons;
using Services.Contratos;
using Services.Persona;
using Services.Plan;
using Services.Saldos;
using Services.SaldoXPersona;
using Services.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Xml.Linq;

namespace iZathfit.Views.Pages.SubPagesHome
{
    public partial class SubHomeViewmodel : ObservableObject
    {
        IPersonaService? _personaservice;
        IContratosService? _contratosService;
        ISaldoService? _saldoService;
        ISaldoXPersonaService? _saldoXPersonaService;
        IPlanService? _PlanService;
        localDialogService? _dialog;

        public SubHomeViewmodel()
        {
            _personaservice = App.GetService<IPersonaService>();
            _contratosService = App.GetService<IContratosService>();
            _saldoService = App.GetService<ISaldoService>();
            _saldoXPersonaService = App.GetService<ISaldoXPersonaService>();
            _dialog = App.GetService<localDialogService>();
            _PlanService = App.GetService<IPlanService>();
        }

        [ObservableProperty]
        UserControl? _datausercontrol;

        [ObservableProperty]
        int? _columns = 4;

        [ObservableProperty]
        Visibility _visibletextosbuttons = Visibility.Visible;

        [ObservableProperty]
        Dock _dockPanels = Dock.Left;

        [ObservableProperty]
        HorizontalAlignment _aligmentButtons = HorizontalAlignment.Left;

        [ObservableProperty]
        Visibility _visibleRelojPanel = Visibility.Visible;

        [ObservableProperty]
        bool _lastchild = true;

        [ObservableProperty]
        DataViewUserModel? _dataUserModel;

        [ObservableProperty]
        ComboBoxModelBasic? _SelectItemCombo;

        [ObservableProperty]
        ObservableCollection<ComboBoxModelBasic> _ComboBoxModelBasic = new ObservableCollection<ComboBoxModelBasic>() {
            new ComboBoxModelBasic(){ Id = 1, Name ="Fecha " }
        };


        public List<ContratosModDataView> _contratosList = new List<ContratosModDataView>();

        [ObservableProperty]
        ObservableCollection<ContratosModDataView>? _ContratosObser;

        public StackPanel? RelojPanel { get; set; }
        public async Task ViewDataUser(string Identificacion) {
            if (_personaservice == null || _contratosService == null
                || _saldoService == null || _saldoXPersonaService == null || _dialog == null
                || _PlanService == null) return;
            var contratos = await _contratosService.SelectOneContratoPerDNIPerson(Identificacion);

            if (contratos == null) {
                _dialog.ShowDialog("No hay Datos para esta Persona", owner: App.GetService<MainWindow>());
                return;
            }
            var persona = await _personaservice.GetPersona(contratos[0].IdPersona);
            var saldosxpersonas = await _saldoXPersonaService.GetSaldosXPersonasbyPersona(persona.IdPersona);
            if (saldosxpersonas != null)
            {
                var planes = await _PlanService.GetPlanes();
                var contratosmod = from ct in contratos
                                   join sald in saldosxpersonas
                                   on ct.IdContrato equals sald.IdContrato
                                   select new ContratosModDataView()
                                   {
                                       NombreContrato = ct.GetNombreContrato,
                                       FechaContrato = ct.Fecha_contrato,
                                       FechaFinal = ct.FechaFinal,
                                       PagoFaltante = sald.TotalFaltante,
                                       PrecioTotal = ct.ValorTotal,
                                       UltimoPago = saldosxpersonas.Where(x => x.IdContrato == ct.IdContrato)
                                                    .OrderByDescending(x => x.FechaPago).First().TotalPagadoActual,
                                       Servicios = planes.First(y => y.IdPlanes == ct.IdPlan).GetServicios,
                                       NombrePlan = planes.First(x => x.IdPlanes == ct.IdPlan).GetTitulo
                                   };
                _contratosList = contratosmod.DistinctBy(x => x.NombreContrato).AsList();
                ContratosObser = new ObservableCollection<ContratosModDataView>(_contratosList);
            }


            /*
                Nombre de Contrato,
                Precio Total,
                Servicios,
                Ultimo Pago,
                Fecha de contrato,
                PagoFaltante
                FechaFinal
                Filtros: Pagados, Adeudos, Vencidos Por Pagar
                Filtrar 2: Fecha, NumeroContrato, Descendente, Ascendente
             */
            var estadisticassaldos = await _saldoService.GetEstadisticas(persona.IdPersona);
            var estadisticassaldosxpersona = await _saldoXPersonaService.GetEstadisticas(persona.IdPersona);
            DataUserModel = new DataViewUserModel() {
                SaldosEstadisticasDTO = estadisticassaldos,
                SaldosXpersonaEstidisticas = estadisticassaldosxpersona,
                Persona = persona,
                Contratoscontrat = contratos.Count()
            };

            var datauser = App.GetService<ClienteDataView>();
            if (datauser != null)
            {
                VisibleRelojPanel = Visibility.Collapsed;
                datauser.DataContext = this;
                Datausercontrol = datauser;
                Wpf.Ui.Animations.Transitions.ApplyTransition(Datausercontrol, Wpf.Ui.Animations.TransitionType.SlideLeft, 300);
            }
        }

        public ObservableCollection<ContratosModDataView>? Ordenar(int opc) {
            var lista = opc switch
            {
                1 => new ObservableCollection<ContratosModDataView>(_contratosList.Where(x => x.PagoFaltante == 0)),
                2 => new ObservableCollection<ContratosModDataView>(_contratosList.Where(x => x.PagoFaltante != 0 && DateTime.Now.Date <= x.FechaFinal)),
                3 => new ObservableCollection<ContratosModDataView>(_contratosList.Where(x => x.PagoFaltante != 0 && DateTime.Now.Date > x.FechaFinal)),
                _ => null
            };
            return lista;
        }

        [RelayCommand]
        void ViewRelojPanel() {
            if (RelojPanel != null)
            {
                VisibleRelojPanel = Visibility.Visible;
                Datausercontrol = null;
                Wpf.Ui.Animations.Transitions.ApplyTransition(RelojPanel, Wpf.Ui.Animations.TransitionType.SlideRight, 300);
            }
            
        }

    }
}
