using Configuration;
using iZathfit.Helpers;
using iZathfit.ViewModels.Pages;
using iZathfit.Views.Windows;
using Models;
using Services.Genero;
using Services.Persona;
using Services.Usuario;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Components.ElementosUsuario
{
    public partial class DatosPerfilViewModel : ObservableObject
    {
        IPersonaService? _personaservice;
        IGeneralConfiguration? _config;
        localDialogService? _dialog;
        IExceptionHelperService? _helperexcep;
        IUsuarioService? _usuarioService;
        IGeneroService? _generoService;

        public DatosPerfilViewModel()
        {
            _personaservice = App.GetService<IPersonaService>();
            _config = App.GetService<IGeneralConfiguration>();
            _dialog = App.GetService<localDialogService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
            _usuarioService = App.GetService<IUsuarioService>();
            _generoService = App.GetService<IGeneroService>();
        }

        [ObservableProperty]
        string? _nombres = "";

        [ObservableProperty]
        string? _apellidos = "";

        [ObservableProperty]
        DateTime? _fechanacimiento = null;

        [ObservableProperty]
        string? _direccion = "";

        [ObservableProperty]
        string? _numerotelefono = "";

        [ObservableProperty]
        string? _email = "";

        [ObservableProperty]
        string? _contraseñaantigua = "";

        [ObservableProperty]
        string? _contraseñanueva = "";

        [ObservableProperty]
        string? _contraseñarepetidanueva = "";

        [ObservableProperty]
        GeneroModel? _generoselected;

        [ObservableProperty]
        ObservableCollection<GeneroModel>? _generolist;

        public async Task GetData(UiWindow? win) {
            if (_helperexcep == null || _generoService == null) return;
            var result = await _helperexcep.ExcepHandler(() => _generoService.GetGeneros(), win != null ? win : App.GetService<MainWindow>());
            if (result == null) return;
            Generolist = new ObservableCollection<GeneroModel>(result);
        }

        public async Task<bool> GuardarDatos(Action ChangeDatosUsuario, UiWindow win) {
            if (_personaservice == null || _config == null || _dialog == null || _helperexcep == null) return false;
            if(!Validar(win)) return false;
            if(string.IsNullOrWhiteSpace(Email)) if(_dialog?.ShowDialog("Desea dejar Email vacio?, puede necesitarlo para recuperar la contraseña",
                ShowCancelButton: true,aceptarbutton: "si", cancelarButton: "No",owner: win) == false) return false;
            var user = _config.getuserSistema();
            if(user == null) return false;
            if (_dialog?.ShowConfirmPassword("Confirme su contraseña actual", user.contrasena, owner: win) == false) return false;
            var persona = new PersonaModel()
            {
                Nombres = Nombres,
                Apellidos = Apellidos,
                Fech_Nacimiento = Fechanacimiento.Value.Date,
                Direccion = Direccion,
                Telefono = Numerotelefono,
                Email = Email,
                IdPersona = user.IdPersona,
                Genero = Generoselected.descripcion,
                IdGenero = Generoselected.IdGenero
            };
            var result = await _helperexcep.ExcepHandler(() => _personaservice.UpdatePersonaSistema(persona), win);
            _dialog?.ShowDialog(result ? "Persona Actualizada" : "Persona Rechazada", owner: win);
            if (result)
            {
                user.Nombres = persona.Nombres;
                user.Apellidos = persona.Apellidos;
                user.Fech_Nacimiento = persona.Fech_Nacimiento.Date;
                user.Direccion = persona.Direccion;
                user.Telefono = persona.Telefono;
                user.Email = persona.Email;
                user.generoCode = Generoselected.code;
                user.GeneroDescripcion = Generoselected.descripcion; 
                ChangeDatosUsuario();
            }
            return result;
        }

        public async Task<bool> ChangePassword(UiWindow win) {
            if (_usuarioService == null || _config == null || _dialog == null || _helperexcep == null) return false;
            if (!validarcontraseña(win)) return false;
            var user = _config.getuserSistema();
            if (user == null) return false;
            if (_dialog?.ShowConfirmPassword("Confirme su contraseña actual", user.contrasena, owner: win) == false) return false;
            var result = await _helperexcep.ExcepHandler(() => _usuarioService.CambiarContraseña(Contraseñanueva, user.IdPersona), win);
            _dialog?.ShowDialog(result != 0 ? "Contraseña Actualizada" : "Contraseña Rechazada", owner: win);
            if (result != 0)
            {
                user.contrasena = Contraseñanueva;
            }
            return result != 0;
        }


        bool validarcontraseña(UiWindow win)
        {
            if (string.IsNullOrWhiteSpace(Contraseñaantigua))
            {
                _dialog?.ShowDialog("Contraseña Antigua o Vieja esta vacia", owner: win);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Contraseñanueva))
            {
                _dialog?.ShowDialog("Contraseña nueva esta vacia", owner: win);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Contraseñarepetidanueva))
            {
                _dialog?.ShowDialog("Repita la contraseña", owner: win);
                return false;
            }
            if (Contraseñanueva != Contraseñarepetidanueva)
            {
                _dialog?.ShowDialog("Las Contraseñas nuevas no coinciden", owner: win);
                return false;
            }
            return true;
        }


        bool Validar(UiWindow win) {
            if (string.IsNullOrWhiteSpace(Nombres))
            {
                _dialog?.ShowDialog("Nombres esta vacio", owner: win);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Apellidos))
            {
                _dialog?.ShowDialog("Apellidos esta vacio", owner: win);
                return false;
            }
            if (Fechanacimiento == null)
            {
                _dialog?.ShowDialog("Seleccione una Fecha de Nacimiento", owner: win);
                return false;

            }
            if (string.IsNullOrWhiteSpace(Numerotelefono)) {
                _dialog?.ShowDialog("Numero de Telefono esta vacio", owner: win);
                return false;
            }
            if (Generoselected == null)
            {
                _dialog?.ShowDialog("Seleccione un Genero", owner: win);
                return false;
            }

            return true;
        }
    }
}
