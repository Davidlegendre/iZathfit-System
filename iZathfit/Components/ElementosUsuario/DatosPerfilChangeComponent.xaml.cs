using Models;
using Models.DTOS;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace iZathfit.Components.ElementosUsuario
{
    /// <summary>
    /// Lógica de interacción para DatosPerfilChangeComponent.xaml
    /// </summary>
    public partial class DatosPerfilChangeComponent : UserControl
    {
        DatosPerfilViewModel? _vm;
        public DatosPerfilChangeComponent()
        {
            InitializeComponent();
            _vm = DataContext as DatosPerfilViewModel;
        }

        UsuarioSistema? _usuariosistema;
        public UsuarioSistema? PersonaModel { get => _usuariosistema;
            set {
                _usuariosistema = value;
                _vm.Nombres = PersonaModel?.Nombres;
                _vm.Apellidos = PersonaModel?.Apellidos;
                _vm.Fechanacimiento = PersonaModel?.Fech_Nacimiento;
                dpfechanacimiento.DateSelect = PersonaModel.Fech_Nacimiento;
                _vm.Direccion = PersonaModel?.Direccion;
                _vm.Numerotelefono = PersonaModel?.Telefono;
                _vm.Email = PersonaModel?.Email;
                txtnombres.Focus();
            }
        }

        public Action ChangeUserAction { get; set; }
        public UiWindow Win { get; set; }

        private async void btnguardar_click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                _vm.Fechanacimiento = dpfechanacimiento.DateSelect;
                if (await _vm.GuardarDatos(ChangeUserAction, Win) == true) Win.Close();
            }
        }
    }
}
