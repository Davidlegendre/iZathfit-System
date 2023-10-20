using Configuration.GlobalHelpers;
using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Services.Persona;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Pages
{
    public partial class MantenimientoPersonasVM : ObservableObject, IDisposable
    {
        IPersonaService? _servicio;
        IExceptionHelperService? _handlex;
        localDialogService? _dialog;
        public MantenimientoPersonasVM()
        {
            _servicio = App.GetService<IPersonaService>();
            _handlex = App.GetService<IExceptionHelperService>();
            _dialog = App.GetService<localDialogService>();
        }

        [ObservableProperty]
        ObservableCollection<PersonaModel>? _personas = new ObservableCollection<PersonaModel>();

        public List<PersonaModel>? _personaslist = new List<PersonaModel>();

        [ObservableProperty]
        int _columns = 4;

        public async Task ObtenerPersonas() {
            if (_servicio == null || _handlex == null) return;
            var result = await _handlex.ExcepHandler(() => _servicio.SelectAllPersonasNormal(), App.GetService<MainWindow>());
            if (result == null) { _personaslist.Clear(); return; }
            _personaslist = result;

        }

        public async Task EliminarPersona(PersonaModel? persona, UiWindow win)
        {
            if (persona == null || _handlex == null || _servicio == null) return;
            if (_dialog?.ShowDialog("Desea Eliminar a la persona: " + persona.GetCompleteName + "?", ShowCancelButton: true) == true)
            {
                var result = await _handlex.ExcepHandler(() => _servicio.DeletePersona(persona.IdPersona), win);
                if (result > 0)
                    _personaslist.Remove(persona);
            }
        }

        public void VerDatosPersona(PersonaModel? persona)
        { 
            if(persona == null) return;
            _dialog?.ShowDialog("Nombres: " + persona.Nombres + "\n" + "Apellidos: " +
                persona.Apellidos + "\n" + "Identificacion: " 
                + persona.Identificacion + "\n" + "Fecha Nacimiento: " 
                + persona.Fech_Nacimiento.ToShortDateString() + "\n" 
                + persona.GetEdad + "\n" + persona.GetDireccion + "\n" 
                + persona.GetTelefono + "" + "\n" +
                "Email: " + persona.Email + "\n" 
                + "Tipo Identificacion: " + persona.TipoIdentificacion + "\n"+
                "Genero: " + persona.Genero + "\n" + "Rol: " + persona.Rol + "\n" + "Ocupacion: " + persona.Ocupacion + "\n" + 
                "Numeros de Emergencias: " + persona.NumeroEmergencia1 + ", " + persona.NumeroEmergencia2
               , titulo: "Datos de: " + persona.Nombres.Split(' ')[0]);
        }

        public void Dispose()
        {
            Personas = null;
            _personaslist = null;
        }
    }
}
