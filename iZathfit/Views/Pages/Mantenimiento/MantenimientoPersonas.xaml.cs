using Configuration.GlobalHelpers;
using Dapper;
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
    /// Lógica de interacción para MantenimientoPage.xaml
    /// </summary>
    public partial class MantenimientoPersonas : UserControl, IDisposable
    {
        MantenimientoPersonasVM? _vm;
        ObservableCollection<PersonaModel>? _peopleCopy;
        IGlobalHelpers? _helpers;
        public MantenimientoPersonas()
        {
            InitializeComponent();
            _vm = this.DataContext as MantenimientoPersonasVM;
            _helpers = App.GetService<IGlobalHelpers>();
            this.Loaded += MantenimientoPersonas_Loaded;
            this.SizeChanged += MantenimientoPersonas_SizeChanged;
        }

        private void MantenimientoPersonas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_vm != null && _helpers != null)
            {
                _vm.Columns = _helpers.ColumnsFromWidthWindow(Convert.ToInt32(this.ActualWidth));
            }
        }

        private async void MantenimientoPersonas_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 500);
            if (_vm != null)
            {
                await _vm.ObtenerPersonas();
                _peopleCopy = new ObservableCollection<PersonaModel>(_vm._personaslist);
                _vm.Personas = paginator.GetPaginationCollection(_peopleCopy);
            }
            
        }

        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null || _peopleCopy == null) return;
            new MPersonasForm(_vm._personaslist).ShowDialog();
            _peopleCopy = new ObservableCollection<PersonaModel>(_vm._personaslist);
            _vm.Personas = paginator.GetPaginationCollection(_peopleCopy);
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as PersonaModel;
            if (context != null && _vm != null && _peopleCopy != null)
            {
                await _vm.EliminarPersona(context, App.GetService<MainWindow>());
                _peopleCopy = new ObservableCollection<PersonaModel>(_vm._personaslist);
                _vm.Personas = paginator.GetPaginationCollection(_peopleCopy);
            }
        }

        private void btnshow_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as PersonaModel;
            if (context != null && _vm != null)
            {
                _vm.VerDatosPersona(context);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as PersonaModel;
            if (context != null && _vm != null && _peopleCopy != null)
            {
                new MPersonasForm(_vm._personaslist, persona: context).ShowDialog();
                _peopleCopy = new ObservableCollection<PersonaModel>(_vm._personaslist);
                _vm.Personas = paginator.GetPaginationCollection(_peopleCopy);
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                if (_vm == null || _peopleCopy == null) return;
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                { }
                else
                {
                    _vm.Personas = paginator.BuscarWithPagination(_peopleCopy.Where(x =>
                        x.GetCompleteName.ToLower().Contains(txtBuscar.Text.ToLower()) ||
                        x.Identificacion.Contains(txtBuscar.Text) ||
                        x.Rol.ToLower().Contains(txtBuscar.Text.ToLower())));
                }
            }
        }

        private void paginator_ChangePageEvent(object sender, EventArgs e)
        {
            if (_vm != null && _peopleCopy != null)
            {
                _vm.Personas = paginator.GetPaginationCollection(_peopleCopy);
            }
        }

        public void Dispose()
        {
            _vm?.Dispose();
            _helpers = null;
            _peopleCopy = null;
        }
    }
}
