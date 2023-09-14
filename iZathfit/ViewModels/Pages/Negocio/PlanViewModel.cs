using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Services.Plan;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Pages.Negocio
{
    public partial class PlanViewModel : ObservableObject
    {
        IPlanService? _servicio;
        IExceptionHelperService? _helperexcep;
        localDialogService? _dialog;

        public PlanViewModel()
        {
            _servicio = App.GetService<IPlanService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
            _dialog = App.GetService<localDialogService>();
        }

        [ObservableProperty]
        ObservableCollection<PlanModel>? _planes;

        public async Task<bool> CargarDatos() { 
            if(_servicio == null || _helperexcep == null) return false;
            var result = await _helperexcep.ExcepHandler(() => _servicio.GetPlanes(), App.GetService<MainWindow>());
            Planes = result != null ? new ObservableCollection<PlanModel>(result) : null;
            return result != null;
        }

        public async Task<bool> Eliminar(UiWindow win, Models.PlanModel planmodel)
        {
            if (_servicio == null || _helperexcep == null || _dialog == null) return false;

            var result = await _helperexcep.ExcepHandler(() => _servicio.DeletePlan(planmodel.IdPlanes), win);

            _dialog.ShowDialog(result ? "Plan Eliminado" : "Eliminacion Rechazada");


            return result;
        }

        public void ViewData(PlanModel plan) {
            if (_dialog == null) return;
            _dialog.ShowDialog(mensaje: plan.GetTitulo + "\nPrecio: " + plan.GetPrecioString + 
                "\nServicios: " + plan.GetServicios + "\nDuracion: " + plan.GetMesesTiempoString
                ,titulo: "Datos del Paquete", owner: App.GetService<MainWindow>());
        }

    }
}
