using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Mantenimiento.WindowSecundarios
{
    /// <summary>
    /// Lógica de interacción para MUsuarioForm.xaml
    /// </summary>
    public partial class MUsuarioForm : UiWindow
    {
        Models.Usuario? usuario;
        bool resultdialog = false;
        MUsuarioFormVM? _vm;
        ObservableCollection<Models.Usuario> _users;
        public MUsuarioForm(ObservableCollection<Models.Usuario> usuarios,Models.Usuario? usuario = null)
        {
            InitializeComponent();
            this.usuario = usuario;
            this.Loaded += MUsuarioForm_Loaded;
            this.Closing += MUsuarioForm_Closing;
            _vm = this.DataContext as MUsuarioFormVM;
            _users = usuarios;
        }

        private void MUsuarioForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = resultdialog;
        }

        private async void MUsuarioForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            TBTitulo.Title = usuario == null ? "Agregar Usuario" : "Modificar Usuario";

            if (await _vm.CargarDatos(this,usuario) == false)
                this.Close();
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txttoggle_Checked(object sender, RoutedEventArgs e)
        {
            txttoggle.Content = "Activo";
        }

        private void txttoggle_Unchecked(object sender, RoutedEventArgs e)
        {
            txttoggle.Content = "InActivo";
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            _vm.Password = txtPassword.Password;
            var result = usuario == null ? await _vm!.GuardarUsuario(this, _users) :
               await _vm!.ActualizarUsuario(usuario.idUsuario,this, _users);
            if (result)
            {
                resultdialog = true;
                this.Close();
            }
        }
    }
}
