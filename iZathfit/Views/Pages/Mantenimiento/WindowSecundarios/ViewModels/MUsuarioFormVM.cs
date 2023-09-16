using Configuration;
using Configuration.GlobalHelpers;
using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Services.Genero;
using Services.Persona;
using Services.Rol;
using Services.TipoIdentificacion;
using Services.Usuario;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels
{
    public partial class MUsuarioFormVM : ObservableObject
    {
        ITipoIdentificacionService? _TipoIdentificacionService;
        localDialogService? _dialog;
        IPersonaService? _personaService;
        IGlobalHelpers? _helpService;
        IExceptionHelperService? _helperex;
        IGeneralConfiguration? _config;
        IUsuarioService? _usuarioService;
        
        public MUsuarioFormVM()
        {
            _TipoIdentificacionService = App.GetService<ITipoIdentificacionService>();
            _dialog = App.GetService<localDialogService>();
            _personaService = App.GetService<IPersonaService>();
            _helperex = App.GetService<IExceptionHelperService>();
            _helpService = App.GetService<IGlobalHelpers>();
            _config = App.GetService<IGeneralConfiguration>();
            _usuarioService = App.GetService<IUsuarioService>();
        }

        [ObservableProperty]
        ObservableCollection<Models.PersonaModel>? _personaList;

        [ObservableProperty]
        ObservableCollection<Models.Usuario>? _usuarioList;

        [ObservableProperty]
        bool _isActivo = true;

        [ObservableProperty]
        string _password = "";

        [ObservableProperty]
        string _usuario = "";

        [ObservableProperty]
        PersonaModel? _persona = null;

        [ObservableProperty]
        Visibility _limpiarbtnVisible = Visibility.Visible;

        public async Task<bool> CargarDatos(UiWindow win,Models.Usuario? usermod = null) {
           

            if (_TipoIdentificacionService == null || _personaService == null || _usuarioService == null) return false;
            var personas = await _personaService.SelectAllPersonasNormal();

            if (personas == null || personas.Count() == 0)
            {
                _dialog?.ShowDialog(
                mensaje: "No Puede Acceder, faltan: Personas",
                titulo: "Ups", owner: win); 
                return false;
            }
            //los clientes no tienen usuario
            personas = personas.Where(x => x.CodeRol != _config.GetRol(TypeRol.Cliente)).ToList();

            PersonaList = new ObservableCollection<PersonaModel>(personas);

            //selecciona persona si hay

            if (usermod != null)
            {
                IsActivo = usermod.IsActivo;
                Usuario = usermod.usuario;
                Password = usermod.contrasena;
                Persona = PersonaList.First(x => x.IdPersona == usermod.IdPersona);
                LimpiarbtnVisible = Visibility.Collapsed;
            }
            return true;

        }

        public void Limpiar() {
            IsActivo = true;
            Usuario = "";
            Password = "";
            Persona = null;
        }

        public async Task<bool> GuardarUsuario(UiWindow win, ObservableCollection<Models.Usuario> lista) {
            var usuario = _config?.getuserSistema();
            if (!Verificar() || _helperex == null || _personaService == null || usuario == null 
                || _usuarioService == null)
                return false;
            
            if (_dialog?.ShowConfirmPassword(mensaje:"Hola, confirme la contraseña primero: ", 
                titulo:"Credenciales", contrasena: usuario.contrasena, owner: win) == false)
            { _dialog?.ShowDialog("Contraseña Invalida", owner: win); return false; }

            var usernew = new Models.Usuario() { 
                IdPersona = Persona.IdPersona,
                usuario = Usuario,
                contrasena = Password
            };

            var result = await _helperex.ExcepHandler(() => _usuarioService.InsertarUsuario(usernew), win);
            if (result != null)
            {
                _dialog?.ShowDialog("Usuario Ingresado correctamente", owner: win);
                lista.Add(result);
            }
            return result != null;
        }

        public async Task<bool> ActualizarUsuario(Guid IdUsuario,UiWindow win,
            ObservableCollection<Usuario> lista)
        {
            var usuario = _config?.getuserSistema();
            if (!Verificar() || _helperex == null || _personaService == null || usuario == null
                || _usuarioService == null)
                return false;

            if (_dialog?.ShowConfirmPassword(mensaje: "Hola, confirme la contraseña primero: ", 
                titulo: "Credenciales", contrasena: usuario.contrasena, owner: win) == false)
            {
                _dialog?.ShowDialog("Contraseña Invalida", owner: win);
                return false;
            }

            var usernew = new Models.Usuario()
            {
                idUsuario = IdUsuario,
                IdPersona = Persona.IdPersona,
                usuario = Usuario,
                contrasena = Password,
                IsActivo = IsActivo
            };

            var result = await _helperex.ExcepHandler(()=> _usuarioService.UpdateUsuario(usernew), win);
            if (result != null)
            {
                _dialog?.ShowDialog("Usuario Modificado correctamente", owner: win);
                lista.Remove(lista.First(x => x.idUsuario == result.idUsuario));
                lista.Insert(0, result);
            }
            return result != null;

        }


        bool Verificar() {
            if (string.IsNullOrWhiteSpace(Usuario))
            {
                _dialog?.ShowDialog("Usuario no tiene valor");
                return false;
            }
            if(string.IsNullOrWhiteSpace(Password))
            {
                _dialog?.ShowDialog("Password no tiene valor");
                return false;
            }
            if (Persona == null)
            {
                _dialog?.ShowDialog("No hay una Persona Seleccionada");
                return false;
            }
            return true;
        }
        
    }
}
