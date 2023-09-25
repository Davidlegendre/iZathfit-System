using Dapper;
using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models.DTOS;
using Services.PadecimientosEnfermedades;
using Services.Persona;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iZathfit.ViewModels.Pages
{
    public partial class MantenimientoPadecimientodEnfermedadesVM : ObservableObject, IDisposable
    {
        IPadecimientosEnfermedadesService? _servicio;
        IPersonaService? _personaservice;
        IExceptionHelperService? _exceptionhelper;
        localDialogService? _dialog;
        public MantenimientoPadecimientodEnfermedadesVM()
        {
            _servicio = App.GetService<IPadecimientosEnfermedadesService>();
            _dialog = App.GetService<localDialogService>();
            _personaservice = App.GetService<IPersonaService>();
            _exceptionhelper= App.GetService<IExceptionHelperService>();
        }

        [ObservableProperty]
        ObservableCollection<PadecimientosEnfermedadesDTO>? _padecimientos;


        public List<PadecimientosEnfermedadesDTO>? _padecimientoslist = new List<PadecimientosEnfermedadesDTO>();

        [ObservableProperty]
        int _columns = 4;

        public async Task<bool> CargarDatos() { 
           if(_servicio == null || _personaservice == null || _dialog == null) return false;
            var countpersonas = await _personaservice.GetCountPersonas();
            if (countpersonas == 0)
            {
                _dialog.ShowDialog("Necesita Personas Primero");
                return false;
            }

            var padecimientos = await _servicio.GetAll();
            _padecimientoslist.Clear();
            if (padecimientos != null && padecimientos.Count() != 0)
            {                //si hay padecimientos  los agrupo por idpersona
                /*
                    luego agrego las enfermedades y busco datos de esa persona
                luego los agrego al dto dentro de la coleccion
                 */                
                var agrupado = padecimientos.GroupBy(x => x.IdPersona).AsList();
                foreach (var x in agrupado)
                {
                    var enfermedades = new List<string>();
                    x.AsList().ForEach(padecimiento =>
                    {
                        enfermedades.Add(padecimiento.Padecimiento);
                    });
                    var persona = await _personaservice.GetPersona(x.Key);

                    _padecimientoslist.Add(new PadecimientosEnfermedadesDTO()
                    {
                        Idpersona = x.Key,
                        Enfermedades = enfermedades,
                        Persona = persona
                    });
                }

            }
            return true;
        }

        public void Ver(PadecimientosEnfermedadesDTO enfermedadesDTO) {
            var enfermedades = string.Join(", ", enfermedadesDTO.Enfermedades);
            _dialog?.ShowDialog("Estas son los datos de: " + enfermedadesDTO.Persona.GetCompleteName +
                "\n\n" + "Enfermedades y Padecimientos: " + enfermedades
                , owner: App.GetService<MainWindow>());
        }

        public async Task<bool> Eliminar(PadecimientosEnfermedadesDTO enfermedadesDTO) {
            if (_exceptionhelper == null || _servicio == null) return false;
            var result = await _exceptionhelper.ExcepHandler(() => _servicio.DeletePadecimientosEnfermedades(enfermedadesDTO.Idpersona),
                App.GetService<MainWindow>());
            if (result)
            {
                _dialog?.ShowDialog("Padecimientos y Enfermedades eliminadas");
                await CargarDatos();
            }
            else
            {
                _dialog?.ShowDialog("Padecimientos y Enfermedades Rechazadas");
            }
            return result;
        }

        public void Dispose()
        {
            _servicio = null;
            _dialog = null;
            _personaservice = null;
            _exceptionhelper = null;
            _padecimientoslist = null;
            Padecimientos = null;
        }
    }
}
