using Configuration;
using Configuration.GlobalHelpers;
using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Services.Genero;
using Services.Ocupacion;
using Services.Persona;
using Services.Rol;
using Services.TipoIdentificacion;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels
{
    public partial class MPersonasFormVM : ObservableObject
    {
        ITipoIdentificacionService? _TipoIdentificacionService;
        IGeneroService? _generoservice;
        IRolService? _rolservice;
        localDialogService? _dialog;
        IPersonaService? _personaService;
        IGlobalHelpers? _helpService;
        IExceptionHelperService? _helperex;
        IGeneralConfiguration? _config;
        IOcupacionService? _OcupacionService;
        
        public MPersonasFormVM()
        {
            _TipoIdentificacionService = App.GetService<ITipoIdentificacionService>();
            _generoservice = App.GetService<IGeneroService>();
            _rolservice = App.GetService<IRolService>();
            _dialog = App.GetService<localDialogService>();
            _personaService = App.GetService<IPersonaService>();
            _helperex = App.GetService<IExceptionHelperService>();
            _helpService = App.GetService<IGlobalHelpers>();
            _config = App.GetService<IGeneralConfiguration>();
            _OcupacionService = App.GetService<IOcupacionService>();
        }

        [ObservableProperty]
        ObservableCollection<TipoIdentificacionModel>? tipoIdentificacionList;

        [ObservableProperty]
        ObservableCollection<GeneroModel>? generoList;

        [ObservableProperty]
        ObservableCollection<RolModel>? rolList;

        [ObservableProperty]
        ObservableCollection<Ocupacion>? ocupacionList;

        [ObservableProperty]
        string? identificacion = "";
        [ObservableProperty]
        string? nombres = "";
        [ObservableProperty]
        string? apellidos = "";
        [ObservableProperty]
        string? direccion = "";
        [ObservableProperty]
        string? telefono = "";
        [ObservableProperty]
        string? email = "";
        [ObservableProperty]
        DateTime fechnacimiento = DateTime.Now;

        [ObservableProperty]
        string? numemergencia1 = "";
        [ObservableProperty]
        string? numemergencia2 = "";

        [ObservableProperty]
        TipoIdentificacionModel? tipoIdentificacion;

        [ObservableProperty]
        Ocupacion? ocupacionmodel;

        [ObservableProperty]
        GeneroModel? generoModel;

        [ObservableProperty]
        RolModel? rolModel;

        [ObservableProperty]
        Visibility limpiarbtnVisible = Visibility.Visible;

        public async Task<bool> CargarDatos(UiWindow win,PersonaModel? persona = null) {
            if (_TipoIdentificacionService == null || _generoservice == null || _rolservice == null 
                || _OcupacionService == null) return false;
            var tipos = await _TipoIdentificacionService.GetTipoIdentificaciones();
            var gener = await _generoservice.GetGeneros();
            var rol = await _rolservice.GetRoles();
            var ocup = await _OcupacionService.GetOcupaciones();

            if (tipos == null || gener == null || rol == null || ocup == null 
                || ocup.Count() == 0 || tipos.Count() == 0 || gener.Count() == 0 || rol.Count() ==0)
            {
                _dialog?.ShowDialog(
                mensaje: "No Puede Acceder, faltan: Tipo de Indentificacion, Genero o Rol",
                titulo: "Ups", owner: win); return false;
            }

            TipoIdentificacionList = new ObservableCollection<TipoIdentificacionModel>(tipos);
            GeneroList = new ObservableCollection<GeneroModel>(gener);
            RolList = new ObservableCollection<RolModel>(rol);
            OcupacionList = new ObservableCollection<Ocupacion>(ocup);

            if (TipoIdentificacionList != null)
                TipoIdentificacion = persona != null ?
                    tipos.Find(x => x.IdTipoIdentity == persona.IdTipoIdentity) : null;
            if (GeneroList != null) GeneroModel = persona != null ? gener.Find(x => x.IdGenero == persona.IdGenero) : null;
            if (RolList != null) RolModel = persona != null ? rol.Find(x => x.IdRol == persona.IdRol) : null;
            if (OcupacionList != null) Ocupacionmodel = persona != null ? ocup.Find(x => x.IdOcupacion == persona.IdOcupacion) : null;
            if (persona != null)
            {
                Identificacion = persona.Identificacion;
                Nombres = persona.Nombres;
                Apellidos = persona.Apellidos;
                Direccion = persona.Direccion;
                Telefono = persona.Telefono;
                Email = persona.Email;
                Fechnacimiento = persona.Fech_Nacimiento;
                Numemergencia1 = persona.NumeroEmergencia1;
                Numemergencia2 = persona.NumeroEmergencia2;
                LimpiarbtnVisible = Visibility.Collapsed;
            }

            return true;

        }

        public void Limpiar() {
            Nombres = "";
            Apellidos = "";
            Direccion = "";
            Email = "";
            Fechnacimiento = DateTime.Now;
            Identificacion = "";
            GeneroModel = null;
            RolModel = null;
            Ocupacionmodel = null;
            TipoIdentificacion = null;
            Telefono = "";
            Numemergencia1 = "";
            Numemergencia2 = "";
        }

        public async Task<bool> GuardarPersona(UiWindow win, ObservableCollection<PersonaModel> lista) {
            var usuario = _config?.getuserSistema();
            if (!Verificar() || _helperex == null || _personaService == null || usuario == null)
                return false;
            
            if (_dialog?.ShowConfirmPassword(mensaje: "Hola, confirme la contraseña primero: ", 
                titulo: "Credenciales", contrasena: usuario.contrasena, owner: win) == false)
            { _dialog?.ShowDialog("Contraseña Invalida", owner: win); return false; }

            var persona = new PersonaModel() { 
                Nombres = Nombres,
                Apellidos = Apellidos,
                Direccion = Direccion,
                Email= Email,
                Fech_Nacimiento = Fechnacimiento,
                Identificacion = Identificacion,
                IdGenero = GeneroModel.IdGenero,
                IdRol = RolModel.IdRol,
                IdTipoIdentity = TipoIdentificacion.IdTipoIdentity,
                Telefono = Telefono,
                IdOcupacion = Ocupacionmodel.IdOcupacion,
                NumeroEmergencia1 = Numemergencia1,
                NumeroEmergencia2 = Numemergencia2
            };

            var result = await _helperex.ExcepHandler(() => _personaService.InsertarPersona(persona), win);
            if (result != null)
            {
                _dialog?.ShowDialog("Persona Ingresada correctamente", owner: win);
                lista.Add(result);
            }
            return result != null;
        }

        public async Task<bool> ActualizarPersona(UiWindow win, Guid IdPersona, ObservableCollection<PersonaModel> lista)
        {
            var usuario = _config?.getuserSistema();
            if (!Verificar() || _helperex == null || _personaService == null || usuario == null)
                return false;

            if (_dialog?.ShowConfirmPassword(mensaje: "Hola, confirme la contraseña primero: ",
               titulo: "Credenciales", contrasena: usuario.contrasena, owner: win) == false)
            {
                _dialog?.ShowDialog("Contraseña Invalida", owner: win);
                return false;
            }

            var persona = new PersonaModel()
            {
                IdPersona = IdPersona,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Direccion = Direccion,
                Email = Email,
                Fech_Nacimiento = Fechnacimiento,
                Identificacion = Identificacion,
                IdGenero = GeneroModel.IdGenero,
                IdRol = RolModel.IdRol,
                IdTipoIdentity = TipoIdentificacion.IdTipoIdentity,
                Telefono = Telefono,
                IdOcupacion = Ocupacionmodel.IdOcupacion,
                NumeroEmergencia1 = Numemergencia1,
                NumeroEmergencia2 = Numemergencia2
            };
            var result = await _helperex.ExcepHandler(()=> _personaService.UpdatePersona(persona), win);
            if (result != null)
            {
                _dialog?.ShowDialog("Persona Modificada correctamente", owner: win);
                lista.Remove(lista.First(x => x.IdPersona == result.IdPersona));
                lista.Insert(0, result);
            }
            return result != null;

        }


        bool Verificar() {
            if (_helpService == null) return false;
            if (!_helpService.IsNumber(Identificacion))
            {
                _dialog?.ShowDialog("Identificacion es incorrecto o no tiene valor");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Nombres))
            {
                _dialog?.ShowDialog("Nombres no tiene valor");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Apellidos))
            {
                _dialog?.ShowDialog("Apellidos no tiene valor");
                return false;
            }
           
            if (!_helpService.IsNumber(Telefono))
            {
                _dialog?.ShowDialog("Telefono es incorrecto o no tiene valor");
                return false;
            }

            if (!_helpService.IsNumber(Numemergencia1))
            {
                _dialog?.ShowDialog("Numero de Emergencia 1 es incorrecto o no tiene valor");
                return false;
            }

            if (GeneroModel == null)
            {
                _dialog?.ShowDialog("No hay Genero Seleccionado");
                return false;
            }
            if (RolModel == null)
            {
                _dialog?.ShowDialog("No hay un Rol Seleccionado");
                return false;
            }
            if (TipoIdentificacion == null)
            {
                _dialog?.ShowDialog("No hay un Tipo de Indentificacion Seleccionado");
                return false;
            }
            if (Ocupacionmodel == null)
            {
                _dialog?.ShowDialog("No hay una Ocupacion Seleccionada");
                return false;
            }
            return true;
        }
        
    }
}
