using Configuration;
using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Services.Usuario;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Pages
{
    public partial class MantenimientoUsuarioVM : ObservableObject, IDisposable
    {
        IUsuarioService? _service;
        IExceptionHelperService? _helperexc;
        localDialogService? _dialog;
        IGeneralConfiguration? _config;
        public MantenimientoUsuarioVM()
        {
            _service = App.GetService<IUsuarioService>();
            _helperexc = App.GetService<IExceptionHelperService>();
            _dialog = App.GetService<localDialogService>();
            _config = App.GetService<IGeneralConfiguration>();
        }
        [ObservableProperty]
        ObservableCollection<Models.Usuario>? _usuarioList;

        [ObservableProperty]
        int _columns = 4;

        public async Task CargarDatos()
        {
            if (_service == null || _helperexc == null) return;
            var usuarios = await _helperexc.ExcepHandler(() => _service.GetUsuarios(), App.GetService<MainWindow>());
            if (usuarios == null) return;
            UsuarioList = new ObservableCollection<Models.Usuario>(usuarios);
        }

       public void VerUsuarioData(Usuario? usuario) {
            if (usuario == null || _config == null) return;
            if (_dialog?.ShowConfirmPassword(mensaje: "Hola, confirme la contraseña primero: ",
                titulo:"Credenciales", 
                contrasena: _config.getuserSistema()?.contrasena) == true)
                _dialog?.ShowDialog("Usuario: " + usuario.usuario + "\nContraseña: " + usuario.contrasena +
                    "\nPersona: " + usuario.Persona + "\nRol: " + usuario.Rol +
                    "\nEstado: " + usuario.ActivoString, "Datos de: " + usuario.Persona.Split(' ')[0]);
            else
                _dialog?.ShowDialog("Contraseña invalida, no puede ver el usuario");
        }

        public async Task EliminarUsuario(Usuario? user, UiWindow win, ObservableCollection<Usuario> lista)
        {
            if (user == null || _helperexc == null || _service == null) return;
            if (_dialog?.ShowDialog("Desea Eliminar al usuario: " + user.usuario + "?", ShowCancelButton: true) == true)

                if (_dialog?.ShowConfirmPassword(mensaje: "Hola, confirme la contraseña primero: ",
                    titulo: "Credenciales",
                   contrasena: _config.getuserSistema()?.contrasena) == true)
                {
                    var result = await _helperexc.ExcepHandler(() => _service.EliminarUsuario(user.idUsuario), win);
                    if (result != null)
                        lista.Remove(user);
                }
                else
                    _dialog?.ShowDialog("Contraseña Invalida");
        }

        public void Dispose()
        {
            UsuarioList = null;
        }
    }
}
