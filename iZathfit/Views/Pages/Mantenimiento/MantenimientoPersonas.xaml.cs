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
    public partial class MantenimientoPersonas : UserControl
    {
        MantenimientoPersonasVM? _vm;
        ObservableCollection<PersonaModel>? _peopleCopy;
        public MantenimientoPersonas()
        {
            InitializeComponent();
            _vm = this.DataContext as MantenimientoPersonasVM;
            this.Loaded += MantenimientoPersonas_Loaded;
        }

        private async void MantenimientoPersonas_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 500);
            if (_vm != null)
                await _vm.ObtenerPersonas();
        }

        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            if (_peopleCopy != null) { _vm.Personas = _peopleCopy; _peopleCopy = null; };
            new MPersonasForm(_vm.Personas).ShowDialog();
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as PersonaModel;
            if (context != null && _vm != null)
            {
                await _vm.EliminarPersona(context, App.GetService<MainWindow>(), _vm.Personas);
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
            if (context != null && _vm != null)
            {
                if (_peopleCopy != null) { _vm.Personas = _peopleCopy; _peopleCopy = null; };
                if (_vm != null) new MPersonasForm(_vm.Personas, persona: context).ShowDialog();
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                if (_vm == null) return;
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                { if (_peopleCopy != null) _vm.Personas = _peopleCopy; _peopleCopy = null; }
                else
                {
                    if (_peopleCopy == null) _peopleCopy = _vm.Personas;

                    _vm.Personas = new ObservableCollection<PersonaModel>(_peopleCopy.Where(x => 
                        x.GetCompleteName.ToLower().Contains(txtBuscar.Text.ToLower()) || 
                        x.Identificacion.Contains(txtBuscar.Text) || 
                        x.Rol.ToLower().Contains(txtBuscar.Text.ToLower())));
                }
            }
        }
    }
}
