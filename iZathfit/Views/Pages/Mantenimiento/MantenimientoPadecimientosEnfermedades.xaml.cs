using Configuration.GlobalHelpers;
using Dapper;
using iZathfit.ViewModels.Pages;
using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios;
using iZathfit.Views.Windows;
using Models;
using Models.DTOS;
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
    /// Lógica de interacción para MantenimientoPadecimientosEnfermedades.xaml
    /// </summary>
    public partial class MantenimientoPadecimientosEnfermedades : UserControl
    {
        MantenimientoPadecimientodEnfermedadesVM? _vm;
        localDialogService? _dialog;
        ObservableCollection<PadecimientosEnfermedadesDTO>? _copy;
        IGlobalHelpers? _helpers;
        public MantenimientoPadecimientosEnfermedades()
        {
            InitializeComponent();
            _vm = this.DataContext as MantenimientoPadecimientodEnfermedadesVM;
            _helpers = App.GetService<IGlobalHelpers>();
            _dialog = App.GetService<localDialogService>();
            this.Loaded += MantenimientoPadecimientosEnfermedades_Loaded;
            this.SizeChanged += MantenimientoPadecimientosEnfermedades_SizeChanged;
        }

        private void MantenimientoPadecimientosEnfermedades_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_vm != null && _helpers != null)
            {
                _vm.Columns = _helpers.ColumnsFromWidthWindow(Convert.ToInt32(this.ActualWidth));
            }
        }

        private async void MantenimientoPadecimientosEnfermedades_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 500);
            if (_vm == null) return;
            if (await _vm.CargarDatos() == false) return;
            _copy = new ObservableCollection<PadecimientosEnfermedadesDTO>(_vm._padecimientoslist);
            _vm.Padecimientos = paginator.GetPaginationCollection(_copy);
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_vm == null || _copy == null) return;
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                { _vm.Padecimientos = paginator.GetPaginationCollection(_copy); }
                else
                {
                    _vm.Padecimientos = paginator.BuscarWithPagination(_copy.Where(x =>
                        x.Persona.GetCompleteName.ToLower().Contains(txtBuscar.Text.ToLower())
                        || x.Persona.Identificacion.Contains(txtBuscar.Text)));
                }
            }
        }

        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Models.DTOS.PadecimientosEnfermedadesDTO;
            if (context == null || _copy == null) return;
            if (new MPadecimientosEnfermedadesForm(context).ShowDialog() == true)
            {
                await _vm.CargarDatos();
                _copy = new ObservableCollection<PadecimientosEnfermedadesDTO>(_vm._padecimientoslist);
                _vm.Padecimientos = paginator.GetPaginationCollection(_copy);
            }

        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Models.DTOS.PadecimientosEnfermedadesDTO;
            if (context == null || _copy == null ) return;
            if (_dialog?.ShowDialog("Desea eliminar los Padecimientos de: " + context.Persona.GetCompleteName + "?", "Eliminando", true) == true)
            {
               await _vm.Eliminar(context);
                _copy = new ObservableCollection<PadecimientosEnfermedadesDTO>(_vm._padecimientoslist);
                _vm.Padecimientos = paginator.GetPaginationCollection(_copy);
            }
        }

        private void btnshow_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as PadecimientosEnfermedadesDTO;
            if (context != null && _vm != null)
            {
                _vm.Ver(context);
            }
        }

        private async void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            if (_copy == null || _vm == null) return;
            if (new MPadecimientosEnfermedadesForm().ShowDialog() == true)
            {
                await _vm.CargarDatos();
                _copy = new ObservableCollection<PadecimientosEnfermedadesDTO>(_vm._padecimientoslist);
                _vm.Padecimientos = paginator.GetPaginationCollection(_copy);
            }
        }

        private void paginator_ChangePageEvent(object sender, EventArgs e)
        {
            if (_vm != null && _copy != null)
            {
                _vm.Padecimientos = paginator.GetPaginationCollection(_copy);
            }
        }
    }
}
