using iZathfit.Helpers;
using Models;
using Services.Persona;
using Services.PlanDuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Negocio.Ventanas.ViewModels
{
    public class PlanDuracionFormViewModel : IDisposable
    {
        IPlanDuracionService? _servicio;
        localDialogService? _dialog;
        IExceptionHelperService? _helperexcep;
        public PlanDuracionFormViewModel()
        {
            _servicio = App.GetService<IPlanDuracionService>();
            _dialog = App.GetService<localDialogService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
        }

        public async Task<bool> Agregar(UiWindow win, PlanDuracionModel planduracion)
        {
            if (_servicio == null || _dialog == null || _helperexcep == null) return false;
            if (!_dialog.ShowDialog("Desea Ingresar el plan de duracion de: " + planduracion.GetMesesTiempoString + "?", 
                ShowCancelButton: true) == true) return false;

            var result = await _helperexcep.ExcepHandler(() => _servicio.InsertPlanDuracion(planduracion), win);

            _dialog.ShowDialog(result ? "Plan de duracion añadida" : "Plan de duracion rechazada");
            
            return result;
        }

        public async Task<bool> Modificar(UiWindow win, PlanDuracionModel planduracion)
        {
            if (_servicio == null || _dialog == null || _helperexcep == null) return false;
            if (!_dialog.ShowDialog("Desea modificar el plan de duracion: " + planduracion.GetMesesTiempoString + "?", ShowCancelButton: true))
                return false;

            var result = await _helperexcep.ExcepHandler(() => _servicio.UpdatePlanDuracion(planduracion), win);

            _dialog.ShowDialog(result ? "Plan de duracion modificada" : "Plan de duracion rechazada");
            return result;
        }

        public async Task<bool> Eliminar(UiWindow win, PlanDuracionModel planduracion)
        {
            if (_servicio == null || _dialog == null || _helperexcep == null) return false;
            if (!_dialog.ShowDialog("Desea eliminar el plan de duracion: " + planduracion.GetMesesTiempoString + "?", ShowCancelButton: true))
                return false;

            var result = await _helperexcep.ExcepHandler(() => _servicio.DeletePlanDuracion(planduracion.IdPlanDuracion), win);
            _dialog.ShowDialog(result ? "Plan de duracion eliminada" : "Plan de duracion no eliminada");
            return result;
        }

        public void Dispose()
        {
            
        }
    }
}
