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
        IContratosService? _contratosService;
        ISaldoService? _saldoService;
        ISaldoXPersonaService? _saldoXPersonaService;
        IPlanService? _PlanService;
        localDialogService? _dialog;

        public SubHomeViewmodel()
        {
            _contratosService = App.GetService<IContratosService>();
            _saldoService = App.GetService<ISaldoService>();
            _saldoXPersonaService = App.GetService<ISaldoXPersonaService>();
            _dialog = App.GetService<localDialogService>();
            _PlanService = App.GetService<IPlanService>();
        }

        [ObservableProperty]
        UserControl? _datausercontrol;

        [ObservableProperty]
        int? _columns = 3;

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

        public List<ContratosModDataView>? _contratosList = new List<ContratosModDataView>();

        [ObservableProperty]
        ObservableCollection<ContratosModDataView>? _ContratosObser;

        public StackPanel? RelojPanel { get; set; }
        public async Task ViewDataUser(PersonaModel personaModel) {
            if (_contratosService == null
                || _saldoService == null || _saldoXPersonaService == null || _dialog == null
                || _PlanService == null) return;
            var contratos = await _contratosService.SelectOneContratoPerDNIPerson(personaModel.Identificacion);

            //if (contratos == null)
            //{
            //    _dialog.ShowDialog("No hay Datos para esta Persona", owner: App.GetService<MainWindow>());
            //    return;
            //}
            var saldosxpersonas = await _saldoXPersonaService.GetSaldosXPersonasbyPersona(personaModel.IdPersona);
            var planes = await _PlanService.GetPlanes();
            if (contratos != null)
            {
                _contratosList.Clear();
                contratos.ForEach(ct =>
                {
                    var saldo = saldosxpersonas?.Find(y => y.IdContrato == ct.IdContrato);
                    var contratomod = new ContratosModDataView();
                    contratomod.NombreContrato = ct.GetNombreContrato;
                    contratomod.FechaContrato = ct.Fecha_contrato;
                    contratomod.FechaFinal = ct.FechaFinal;
                    contratomod.PagoFaltante = saldo != null ? saldo.TotalFaltante : ct.ValorTotal;
                    contratomod.PrecioTotal = ct.ValorTotal;
                    contratomod.UltimoPago = saldo != null ? saldosxpersonas.Where(x => x.IdContrato == ct.IdContrato)
                                 .OrderByDescending(x => x.FechaPago).First().TotalPagadoActual : 0;
                    contratomod.Servicios = planes.First(y => y.IdPlanes == ct.IdPlan).GetServicios;
                    contratomod.NombrePlan = planes.First(x => x.IdPlanes == ct.IdPlan).GetTitulo;
                    _contratosList.Add(contratomod);
                });
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
            var estadisticassaldos = contratos == null ? new SaldosEstadisticasDTO() : await _saldoService.GetEstadisticas(personaModel.IdPersona);
            var estadisticassaldosxpersona = contratos == null ? new SaldosXpersonaEstidisticas() : await _saldoXPersonaService.GetEstadisticas(personaModel.IdPersona);
            DataUserModel = new DataViewUserModel() {
                SaldosEstadisticasDTO = estadisticassaldos,
                SaldosXpersonaEstidisticas = estadisticassaldosxpersona,
                Persona = personaModel,
                Contratoscontrat = contratos == null ? 0 : contratos.Count()
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
