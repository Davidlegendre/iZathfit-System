using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Services.PlanDuracion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iZathfit.ViewModels.Pages.Negocio
{
    public partial class PlanDuracionPageViewModel : ObservableObject
    {
        IPlanDuracionService? _servicio;
        IExceptionHelperService? _helperexcep;
        public PlanDuracionPageViewModel()
        {
            _servicio = App.GetService<IPlanDuracionService>();
            _helperexcep= App.GetService<IExceptionHelperService>();
        }

        [ObservableProperty]
        ObservableCollection<PlanDuracionModel>? _planduracion;

        public async Task<bool> CargarDatos() {
            if (_servicio == null || _helperexcep == null || _helperexcep == null) return false;
            var result = await _helperexcep.ExcepHandler(() => _servicio.GetPlanesDuracion(), App.GetService<MainWindow>());
            Planduracion = result != null ? new ObservableCollection<PlanDuracionModel>(result) : null;
            return true;
        }
    }
}
