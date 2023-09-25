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
    public partial class MPadecimientosEnfermedadesFormVM : ObservableObject, IDisposable
    {
        IPadecimientosEnfermedadesService? _servicio;
        localDialogService? _dialog;
        IExceptionHelperService? _exceptionHelper;
        public MPadecimientosEnfermedadesFormVM()
        {
            _servicio = App.GetService<IPadecimientosEnfermedadesService>();
            _dialog = App.GetService<localDialogService>();
            _exceptionHelper = App.GetService<IExceptionHelperService>();

      }

        [ObservableProperty]
        ObservableCollection<Models.ModelsCommons.PadecimientoModelSencillo>? _padecimientos = new ObservableCollection<Models.ModelsCommons.PadecimientoModelSencillo>();

        [ObservableProperty]
        PersonaModel? _personamodel;

        [ObservableProperty]
        string? _padecimientotxt = "";

        [ObservableProperty]
        Visibility _limpiarbtnVisible = Visibility.Visible;



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
            if(_exceptionHelper == null) return false;
            if (enfermedadesDTO != null)
            {
                LimpiarbtnVisible = Visibility.Collapsed;
                int num = 0;
                enfermedadesDTO.Enfermedades.ForEach(x => Padecimientos.Add(new Models.ModelsCommons.PadecimientoModelSencillo()
                {
                    id = num++,
                    padecimeinto = x
                }));
            }
            return true;

        }

        //desempaqueto
        /*
            recorro los padecimientos y añado a la lista para enviar
            luego lo envio a insertar
         */
        public async Task<bool> Guardar(UiWindow win) {
            if (_exceptionHelper == null) return false;
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
            if (_exceptionHelper == null) return false;
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

        public void Dispose()
        {
            _servicio = null;
            _dialog = null;
            _exceptionHelper = null;
            Padecimientos = null;
            Personamodel = null;
        }
    }

   
}
