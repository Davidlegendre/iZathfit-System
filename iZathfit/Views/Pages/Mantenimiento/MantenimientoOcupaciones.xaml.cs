using iZathfit.ViewModels.Pages;
using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios;
using iZathfit.Views.Windows;
using Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace iZathfit.Views.Pages.Mantenimiento
{
    /// <summary>
    /// Lógica de interacción para MantenimientoOcupaciones.xaml
    /// </summary>
    public partial class MantenimientoOcupaciones : UserControl
    {
        MantenimientoOcupacionVM? _vm;
        localDialogService? _dialog;
        ObservableCollection<Ocupacion>? _copy;
        public MantenimientoOcupaciones()
        {
            InitializeComponent();
            this.Loaded += MantenimientoOcupaciones_Loaded;
            _vm = this.DataContext as MantenimientoOcupacionVM;
            _dialog = App.GetService<localDialogService>();
        }

        private async void MantenimientoOcupaciones_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 500);
            if (_vm != null) if (await _vm.CargarDatos() == false) return;
            
        }

        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            if (_copy != null) { _vm.Ocupaciones = _copy; _copy = null; };
            new MOcupacionesForm(_vm.Ocupaciones).ShowDialog();
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Models.Ocupacion;
            if(context == null) return;
            if (_dialog?.ShowDialog("Desea eliminar la ocupacion: " + context.descripcion + "?", "Eliminando", true) == true)
            {
                await _vm.Eliminar(context, App.GetService<MainWindow>());
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Models.Ocupacion;
            if(context == null) return;
            if (_copy != null) { _vm.Ocupaciones = _copy; _copy = null; };
            new MOcupacionesForm(_vm.Ocupaciones, context).ShowDialog();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_vm == null) return;
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                { if (_copy != null) _vm.Ocupaciones = _copy; _copy = null; }
                else
                {
                    if (_copy == null) _copy = _vm.Ocupaciones;

                    _vm.Ocupaciones = new ObservableCollection<Ocupacion>(_copy.Where(x =>
                        x.descripcion.ToLower().Contains(txtBuscar.Text.ToLower())));
                }
            }
        }
    }
}
