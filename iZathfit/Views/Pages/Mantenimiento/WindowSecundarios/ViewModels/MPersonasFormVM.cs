using Configuration.GlobalHelpers;
using iZathfit.Helpers;
using Models;
using Services.Genero;
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
        
        public MPersonasFormVM()
        {
            _TipoIdentificacionService = App.GetService<ITipoIdentificacionService>();
            _generoservice = App.GetService<IGeneroService>();
            _rolservice = App.GetService<IRolService>();
            _dialog = App.GetService<localDialogService>();
            _personaService = App.GetService<IPersonaService>();
            _helperex = App.GetService<IExceptionHelperService>();
            _helpService = App.GetService<IGlobalHelpers>();
        }

        

        [ObservableProperty]
        ObservableCollection<TipoIdentificacionModel>? tipoIdentificacionList;

        [ObservableProperty]
        ObservableCollection<GeneroModel>? generoList;

        [ObservableProperty]
        ObservableCollection<RolModel>? rolList;

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
        TipoIdentificacionModel? tipoIdentificacion;

        [ObservableProperty]
        GeneroModel? generoModel;

        [ObservableProperty]
        RolModel? rolModel;

        public async Task CargarDatos(PersonaModel? persona = null) {
            if (_TipoIdentificacionService == null || _generoservice == null || _rolservice == null) return;
            var tipos = await _TipoIdentificacionService.GetTipoIdentificaciones();
            var gener = await _generoservice.GetGeneros();
            var rol = await _rolservice.GetRoles();

            if (tipos == null || gener == null || rol == null)
            { mensaje("No Puede Acceder, faltan: Tipo de Indentificacion, Genero o Rol", "Ups"); return; }

            TipoIdentificacionList = new ObservableCollection<TipoIdentificacionModel>(tipos);
            GeneroList = new ObservableCollection<GeneroModel>(gener);
            RolList = new ObservableCollection<RolModel>(rol);

            if (TipoIdentificacionList != null)
                TipoIdentificacion = persona != null ?
                    tipos.Find(x => x.IdTipoIdentity == persona.IdTipoIdentity) : TipoIdentificacionList[0];
            if (GeneroList != null) GeneroModel = persona != null ? gener.Find(x => x.IdGenero == persona.IdGenero) : GeneroList[0];
            if (RolList != null) RolModel = persona != null ? rol.Find(x => x.IdRol == persona.IdRol) : RolList[0];

            if (persona != null)
            {
                Identificacion = persona.Identificacion;
                Nombres = persona.Nombres;
                Apellidos = persona.Apellidos;
                Direccion = persona.Direccion;
                Telefono = persona.Telefono;
                Email = persona.Email;
                Fechnacimiento = persona.Fech_Nacimiento;
            }

        }

        public async Task<bool> GuardarPersona(UiWindow win, ObservableCollection<PersonaModel> lista) {
            if (!Verificar() || _helperex == null || _personaService == null)
                return false;

            var persona = new PersonaModel() { 
                Nombres = Nombres,
                Apellidos = Apellidos,
                Direccion = string.IsNullOrWhiteSpace(Direccion) ? "-": Direccion,
                Email= string.IsNullOrWhiteSpace(Email) ? "-" : Email,
                Fech_Nacimiento = Fechnacimiento,
                Identificacion = Identificacion,
                IdGenero = GeneroModel.IdGenero,
                IdRol = RolModel.IdRol,
                IdTipoIdentity = TipoIdentificacion.IdTipoIdentity,
                Telefono = string.IsNullOrWhiteSpace(Telefono) ? "-" : Telefono
            };

            var result = await _helperex.ExcepHandler(() => _personaService.InsertarPersona(persona), win);
            if (result != null)
            {
                mensaje("Persona Ingresada correctamente");
                lista.Add(result);
            }
            return result != null;
        }

        public async Task<bool> ActualizarPersona(UiWindow win, Guid IdPersona ,ObservableCollection<PersonaModel> lista)
        {
            if (!Verificar() || _helperex == null || _personaService == null)
                return false;
            var persona = new PersonaModel()
            {
                IdPersona = IdPersona,
                Nombres = Nombres,
                Apellidos = Apellidos,
                Direccion = string.IsNullOrWhiteSpace(Direccion) ? "-" : Direccion,
                Email = string.IsNullOrWhiteSpace(Email) ? "-" : Email,
                Fech_Nacimiento = Fechnacimiento,
                Identificacion = Identificacion,
                IdGenero = GeneroModel.IdGenero,
                IdRol = RolModel.IdRol,
                IdTipoIdentity = TipoIdentificacion.IdTipoIdentity,
                Telefono = string.IsNullOrWhiteSpace(Telefono) ? "-" : Telefono
            };
            var result = await _helperex.ExcepHandler(()=> _personaService.UpdatePersona(persona), win);
            if (result != null)
            {
                mensaje("Persona Modificada correctamente");
                lista.Remove(lista.First(x => x.IdPersona == result.IdPersona));
                lista.Insert(0, result);
            }
            return result != null;

        }


        bool mensaje(string mensaje, string titulo = "Mensaje", bool ShowCancelButton = false) {
            if (_dialog == null) return false;
            return _dialog.ShowDialog(new Models.ModelsCommons.DialogModel() { canShowCancelButton = ShowCancelButton, Title = titulo, Message = mensaje});
        }

        bool Verificar() {
            if (_helpService == null) return false;
            if (!_helpService.IsNumber(Identificacion))
            {
                mensaje("Identificacion es incorrecto o no tiene valor");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Nombres))
            {
                mensaje("Nombres no tiene valor");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Apellidos))
            {
                mensaje("Apellidos no tiene valor");
                return false;
            }
           
            if (!_helpService.IsNumber(Telefono))
            {
                mensaje("Telefono es incorrecto o no tiene valor");
                return false;
            }           

            if (GeneroModel == null)
            {
                mensaje("No hay Genero Seleccionado");
                return false;
            }
            if (RolModel == null)
            {
                mensaje("No hay un Rol Seleccionado");
                return false;
            }
            if (TipoIdentificacion == null)
            {
                mensaje("No hay un Tipo de Indentificacion Seleccionado");
                return false;
            }
            return true;
        }
        
    }
}
