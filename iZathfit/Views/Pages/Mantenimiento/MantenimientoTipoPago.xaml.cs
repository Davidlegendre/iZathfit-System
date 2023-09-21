using Configuration.GlobalHelpers;
using iZathfit.ViewModels.Pages;
using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios;
using iZathfit.Views.Windows;
using Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iZathfit.Views.Pages.Mantenimiento
{
    /// <summary>
    /// Lógica de interacción para MantenimientoTipoPago.xaml
    /// </summary>
    public partial class MantenimientoTipoPago : UserControl
    {
        localDialogService? _dialog;
        MantenimientoTipoPagoViewModel? _vm;
        ObservableCollection<TipoPagoModel>? _copy;
        IGlobalHelpers? _helpers;
        public MantenimientoTipoPago()
        {
            InitializeComponent();
            _helpers = App.GetService<IGlobalHelpers>();    
            _dialog = App.GetService<localDialogService>();
            _vm = DataContext as MantenimientoTipoPagoViewModel;
            this.Loaded += MantenimientoTipoPago_Loaded;
            this.SizeChanged += MantenimientoTipoPago_SizeChanged;
        }

        private void MantenimientoTipoPago_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(_helpers!= null && _vm != null)
            {
                _vm.Columns = _helpers.ColumnsFromWidthWindow(Convert.ToInt32(this.ActualWidth));
            }
        }

        private async void MantenimientoTipoPago_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 500);
            if (_vm != null)
            {
                await _vm.CargarDatos();
               
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_vm == null) return;
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                { if (_copy != null) _vm.Tipopagoslist = _copy; _copy = null; }
                else
                {
                    if (_copy == null) _copy = _vm.Tipopagoslist;

                    _vm.Tipopagoslist = new ObservableCollection<TipoPagoModel>(_copy.Where(x =>
                        x.descripcion.ToLower().Contains(txtBuscar.Text.ToLower())));
                }
            }
        }

        private async void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
             _vm.Tipopagoslist = _copy; _copy = null;
            if (new MTipoPagoForm().ShowDialog() == true)
            {
                _dialog.ShowDialog("Tipo de Pago Guardado", owner: App.GetService<MainWindow>());
                await _vm.CargarDatos();
            }
        }

        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as TipoPagoModel;
            if (context == null) return;
            if (_copy != null) { _vm.Tipopagoslist = _copy; _copy = null; };
            if (new MTipoPagoForm(context).ShowDialog() == true)
            {
                _dialog.ShowDialog("Tipo de Pago Modificado", owner: App.GetService<MainWindow>());
                await _vm.CargarDatos();
            }
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Models.TipoPagoModel;
            if (context == null) return;
            if (_dialog?.ShowDialog("Desea eliminar el Tipo de Pago: " + context.descripcion + "?", "Eliminando", true, owner: App.GetService<MainWindow>()) == true)
            {
                await _vm.Delete(context.IdtipoPago);
            }
        }
    }
}
