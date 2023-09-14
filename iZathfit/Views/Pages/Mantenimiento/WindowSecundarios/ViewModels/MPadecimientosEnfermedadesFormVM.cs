using iZathfit.Helpers;
using Models;
using Services.PadecimientosEnfermedades;
using Services.Persona;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels
{
    public partial class MPadecimientosEnfermedadesFormVM : ObservableObject
    {
        IPadecimientosEnfermedadesService? _servicio;
        localDialogService? _dialog;
        IPersonaService? _persona;
        IExceptionHelperService? _exceptionHelper;
        public MPadecimientosEnfermedadesFormVM()
        {
            _servicio = App.GetService<IPadecimientosEnfermedadesService>();
            _dialog = App.GetService<localDialogService>();
            _persona = App.GetService<IPersonaService>();
            _exceptionHelper = App.GetService<IExceptionHelperService>();

      }

        [ObservableProperty]
        ObservableCollection<PersonaModel> personaslist = new ObservableCollection<PersonaModel>();

        [ObservableProperty]
        ObservableCollection<Models.ModelsCommons.PadecimientoModelSencillo> padecimientos = new ObservableCollection<Models.ModelsCommons.PadecimientoModelSencillo>();

        [ObservableProperty]
        PersonaModel? personamodel;

        [ObservableProperty]
        string? padecimientotxt = "";

        [ObservableProperty]
        Visibility limpiarbtnVisible = Visibility.Visible;



       public void agregarPadecimientoLocal() { 
            if(string.IsNullOrWhiteSpace(Padecimientotxt))
            {
                _dialog?.ShowDialog("Escriba un padecimiento o enfermedad");
                return;
            }

            Padecimientos.Add(new Models.ModelsCommons.PadecimientoModelSencillo() { 
                id = Padecimientos.Count() + 1,
                padecimeinto = Padecimientotxt
            });

            Padecimientotxt = "";
        }

        public void EliminarPadecimientoLocal(Models.ModelsCommons.PadecimientoModelSencillo? padecimientoModelSencillo) {
            if (padecimientoModelSencillo == null) return;
            Padecimientos.Remove(Padecimientos.First(x => x.id == padecimientoModelSencillo.id));

        }

        public async Task<bool> CargarDatos(UiWindow win, Models.DTOS.PadecimientosEnfermedadesDTO? enfermedadesDTO = null) {
            if(_exceptionHelper == null || _persona == null) return false;
            var result = await _exceptionHelper.ExcepHandler(() => _persona.SelectAllAll(), win);
            if (result != null)
            {
                Personaslist = new ObservableCollection<PersonaModel>(result);
                if(enfermedadesDTO != null)
                {
                    LimpiarbtnVisible = Visibility.Collapsed;
                    var persona = Personaslist.First(x => x.IdPersona == enfermedadesDTO.Idpersona);
                    Personamodel = persona;
                    int num = 0;
                    enfermedadesDTO.Enfermedades.ForEach(x => Padecimientos.Add(new Models.ModelsCommons.PadecimientoModelSencillo() {
                        id = num++,
                        padecimeinto = x
                    }));
                }
            }

            return result != null;

        }

        //desempaqueto
        /*
            recorro los padecimientos y añado a la lista para enviar
            luego lo envio a insertar
         */
        public async Task<bool> Guardar(UiWindow win) {
            if (_exceptionHelper == null || _persona == null) return false;
            if (!validar()) return false;
            var lista = new List<PadecimientoEnfermedades>();
            Padecimientos.ToList().ForEach(x => {
                lista.Add(new PadecimientoEnfermedades()
                {
                    IdPersona = Personamodel.IdPersona,
                    Padecimiento = x.padecimeinto
                });
            });
            var result = await _exceptionHelper.ExcepHandler(() => _servicio.InsertarPadecimientosEnfermedades(lista), win);
            if (result)
            {
                _dialog?.ShowDialog("Padecimientos y Enfermedades añadidas");
                return true;
            }
            return result;
        }

        //desempaqueto
        /*
            recorro los padecimientos y añado a la lista para enviar
            luego lo envio a actualizar
         */
        public async Task<bool> Editar(UiWindow win) {
            if (_exceptionHelper == null || _persona == null) return false;
            if (!validar()) return false;
            var lista = new List<PadecimientoEnfermedades>();
            Padecimientos.ToList().ForEach(x => {
                lista.Add(new PadecimientoEnfermedades()
                {
                    IdPersona = Personamodel.IdPersona,
                    Padecimiento = x.padecimeinto
                });
            });
            var result = await _exceptionHelper.ExcepHandler(() => _servicio.UpdatePadecimientosEnfermedades(lista), win);
            if (result)
            {
                _dialog?.ShowDialog("Padecimientos y Enfermedades modificadas");
                return true;
            }
            return result;
        }

        public void limpiar() {
            Personamodel = null;
            Padecimientos.Clear();
            Padecimientotxt = "";
        }

        bool validar() {
            if (Personamodel == null)
            {
                _dialog?.ShowDialog("Seleccione una Persona");
                return false;
            }

            if (Padecimientos.Count() == 0)
            {
                _dialog?.ShowDialog("Agregue al menos un padecimiento");
                return false;
            }
            return true; 
        }
    }

   
}
