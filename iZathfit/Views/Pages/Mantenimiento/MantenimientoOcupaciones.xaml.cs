using Configuration.GlobalHelpers;
using Dapper;
using iZathfit.ViewModels.Pages;
using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios;
using iZathfit.Views.Windows;
using Models;
using Models.DTOS;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace iZathfit.Views.Pages.Mantenimiento
{
    /// <summary>
    /// Lógica de interacción para MantenimientoOcupaciones.xaml
    /// </summary>
    public partial class MantenimientoOcupaciones : UserControl, IDisposable
    {
        MantenimientoOcupacionVM? _vm;
        localDialogService? _dialog;
        ObservableCollection<Ocupacion>? _copy;
        IGlobalHelpers? _helpers;
        public MantenimientoOcupaciones()
        {
            InitializeComponent();
            this.Loaded += MantenimientoOcupaciones_Loaded;
            _helpers = App.GetService<IGlobalHelpers>();
            _vm = this.DataContext as MantenimientoOcupacionVM;
            _dialog = App.GetService<localDialogService>();
            this.SizeChanged += MantenimientoOcupaciones_SizeChanged;
        }

        private void MantenimientoOcupaciones_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           if(_vm != null && _helpers != null) {
                _vm.Columns = _helpers.ColumnsFromWidthWindow(Convert.ToInt32(this.ActualWidth));
            }
        }

        private async void MantenimientoOcupaciones_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 500);
            if (_vm != null) if (await _vm.CargarDatos() == false) return;
            _copy = new ObservableCollection<Ocupacion>(_vm._listaocupacion);
            _vm.Ocupaciones = paginator.GetPaginationCollection(_copy);
        }

        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null || _copy == null) return;
            new MOcupacionesForm(_vm._listaocupacion).ShowDialog();
            _copy = new ObservableCollection<Ocupacion>(_vm._listaocupacion);
            _vm.Ocupaciones = paginator.GetPaginationCollection(_copy);
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Models.Ocupacion;
            if(context == null || _copy == null) return;
            if (_dialog?.ShowDialog("Desea eliminar la ocupacion: " + context.descripcion + "?", "Eliminando", true) == true)
            {
                await _vm.Eliminar(context, App.GetService<MainWindow>());
                _copy = new ObservableCollection<Ocupacion>(_vm._listaocupacion);
                _vm.Ocupaciones = paginator.GetPaginationCollection(_copy);

            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Models.Ocupacion;
            if(context == null || _copy == null) return;
            new MOcupacionesForm(_vm._listaocupacion, ocupacion: context).ShowDialog();
            _copy = new ObservableCollection<Ocupacion>(_vm._listaocupacion);
            _vm.Ocupaciones = paginator.GetPaginationCollection(_copy);
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_vm == null || _copy == null) return;
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                { _vm.Ocupaciones = paginator.GetPaginationCollection(_copy); }
                else
                {
                    _vm.Ocupaciones = paginator.BuscarWithPagination(_copy.Where(x =>
                        x.descripcion.ToLower().Contains(txtBuscar.Text.ToLower())));
                }
            }
        }

        private void paginator_ChangePageEvent(object sender, EventArgs e)
        {
            if (_vm != null && _copy != null)
            {
                _vm.Ocupaciones = paginator.GetPaginationCollection(_copy);
            }
        }

        public void Dispose()
        {
            _vm?.Dispose();
            _helpers = null;
            _dialog = null;
            _copy = null;
        }
    }
}
