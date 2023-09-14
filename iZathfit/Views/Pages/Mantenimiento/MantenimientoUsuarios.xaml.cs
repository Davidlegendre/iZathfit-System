using Configuration;
using iZathfit.ViewModels.Pages;
using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios;
using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels;
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
    /// Lógica de interacción para MantenimientoUsuarios.xaml
    /// </summary>
    public partial class MantenimientoUsuarios : UserControl
    {
        MantenimientoUsuarioVM? _vm;
        ObservableCollection<Models.Usuario>? _copy;
        localDialogService? _dialog;
        IGeneralConfiguration? _config;
        public MantenimientoUsuarios()
        {
            InitializeComponent();
            _vm = this.DataContext as MantenimientoUsuarioVM;
            this.Loaded += MantenimientoUsuarios_Loaded;
            _dialog =App.GetService<localDialogService>();
            _config = App.GetService<IGeneralConfiguration>();
        }

        private async void MantenimientoUsuarios_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 500);
            if (_vm != null) await _vm.CargarDatos();
        }

        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {
            new MUsuarioForm(_vm.UsuarioList).ShowDialog();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_vm == null) return;
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                { if (_copy != null) _vm.UsuarioList = _copy; _copy = null; }
                else
                {
                    if (_copy == null) _copy = _vm.UsuarioList;

                    _vm.UsuarioList = new ObservableCollection<Usuario>(_copy.Where(x =>
                        x.usuario.ToLower().Contains(txtBuscar.Text.ToLower()) ||
                        x.Persona.Contains(txtBuscar.Text) ||
                        x.Rol.ToLower().Contains(txtBuscar.Text.ToLower())));
                }
            }
        }

        private void btnshow_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Models.Usuario;

            if(_vm == null || context == null) return;
            _vm.VerUsuarioData(context);

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Usuario;
            if (context != null && _vm != null)
            {
                if (_copy != null) { _vm.UsuarioList = _copy; _copy = null; };
                if (_dialog?.ShowConfirmPassword(mensaje: "Hola, confirme la contraseña primero: ",
                   titulo: "Credenciales", contrasena: _config.getuserSistema().contrasena,
                   owner: App.GetService<MainWindow>()) == false)
                {
                    _dialog?.ShowDialog("Contraseña Invalida",
                    owner: App.GetService<MainWindow>()); return;

                }
                
                if (_vm != null) 
                    new MUsuarioForm(_vm.UsuarioList, usuario: context).ShowDialog();
            }
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Usuario;
            if (context != null && _vm != null)
            {
                await _vm.EliminarUsuario(context, App.GetService<MainWindow>(), _vm.UsuarioList);
            }
        }

    }
}
