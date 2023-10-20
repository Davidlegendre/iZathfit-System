using Configuration.GlobalHelpers;
using Dapper;
using iZathfit.Helpers;
using Models;
using Services.Plan;
using Services.PlanDuracion;
using Services.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Negocio.Ventanas.ViewModels
{
    public partial class PlanFormViewModel : ObservableObject, IDisposable
    {
        IPlanService? _planservice;
        IServiciosService? _serviciosservice;
        localDialogService? _dialog;
        IExceptionHelperService? _helpexcep;
        IPlanDuracionService? _planduracionservice;
        IGlobalHelpers? _helpers;

        public PlanFormViewModel()
        {
            _planservice = App.GetService<IPlanService>();
            _serviciosservice = App.GetService<IServiciosService>();
            _dialog = App.GetService<localDialogService>();
            _helpexcep = App.GetService<IExceptionHelperService>();
            _helpers = App.GetService<IGlobalHelpers>();
            _planduracionservice = App.GetService<IPlanDuracionService>();
        }

        [ObservableProperty]
        ObservableCollection<ServicioModel>? _servicios;

        [ObservableProperty]
        ObservableCollection<PlanDuracionModel>? _planduraciones;

        [ObservableProperty]
        PlanDuracionModel? _selectedPlanDuracion;

        [ObservableProperty]
        string? _descripcion = "";

        [ObservableProperty]
        string _preciotexto = "";

        public async Task<bool> Cargar(UiWindow win,PlanModel? model = null)
        {
            if (_planservice == null || _serviciosservice == null || _dialog == null || _helpexcep == null || _planduracionservice == null) return false;
            var serv = await _helpexcep.ExcepHandler(() => _serviciosservice.GetServicios(), win);
            var plandur = await _helpexcep.ExcepHandler(() => _planduracionservice.GetPlanesDuracion(), win);
            if (serv == null) {
                _dialog.ShowDialog("No podemos continuar, agregue servicios primero", owner: win);
                return false;
            }
            if(plandur == null)
            {
                _dialog.ShowDialog("No podemos continuar, agregue Planes Duracion primero", owner: win);
                return false;
            }

            Servicios = new ObservableCollection<ServicioModel>(serv);
            Planduraciones = new ObservableCollection<PlanDuracionModel>(plandur);

            /*
                si hay algun servicio entonces procedo a selecciona los servicios
                Servicios tiene InotifyPropertyChanged lo que permite cambiar el estado en la 
                vista automaticamente, lo que hago
                agrego descripcion, agrego el precio y el plan duracion lo filtro por 
                los planes de duracion que tiene el modelo en los planes de duracion original,
                luego los servicios como lista hago match con los idservicio de la lista del modelo
                y activo IsSelected
             */
            if(model != null)
            {
                if (model.Servicios == null || model.Servicios.Count == 0)
                {
                    _dialog.ShowDialog("No podemos continuar, Este Plan NO contiene Servicios agregados", owner: win);
                    return false;
                }
                Descripcion = model.descripcion;
                Preciotexto = model.Precio.ToString();
                SelectedPlanDuracion = plandur.Find(x => x.IdPlanDuracion == model.IdPlanDuracion);
                Servicios.AsList().ForEach(x =>
                {
                    if (model.Servicios.Exists(servicio => servicio.IdServicio == x.IdServicio))
                    { 
                        x.IsSelected = true;
                    }
                });
            }
            return true;

        }

        public async Task<bool> Guardar(UiWindow win) {
            /*
                plan (modelo) uso linq para poder obtener los servicios que tienen IsSelected Activo
                y precio lo convierto a decimal con culture es-PE (perú) (el punto es decimal)
             */
            if (_planservice == null || _serviciosservice == null || _dialog == null || _helpexcep == null || _planduracionservice == null) return false;
            if (!validacion()) return false;
            if (_dialog.ShowDialog("Desea Agregar este Plan/Paquete con precio: " + Decimal.Parse(Preciotexto).ToString("0.00") + " S/?",
                    ShowCancelButton: true, owner: win) == false) return false;

            var plan = new PlanModel() {
                descripcion = Descripcion,
                IdPlanDuracion = SelectedPlanDuracion.IdPlanDuracion,
                Precio = Decimal.Parse(Preciotexto),
                Servicios = Servicios.Where(x => x.IsSelected == true).AsList()
            };

            var result = await _helpexcep.ExcepHandler(() => _planservice.InsertPlan(plan), win);

            _dialog.ShowDialog(result ? "Plan Guardado" : "Plan Rechazado");


            return result;
        }

        public async Task<bool> Modificar(UiWindow win, Models.PlanModel planmodel)
        {
            /*
                plan (modelo) uso linq para poder obtener los servicios que tienen IsSelected Activo
                y precio lo convierto a decimal con culture es-PE (perú) (el punto es decimal), ademas agrego el idplanes
             */
            if (_planservice == null || _serviciosservice == null || _dialog == null || _helpexcep == null || _planduracionservice == null) return false;
            if (!validacion()) return false;
            if (_dialog.ShowDialog("Desea Modificar este Plan/Paquete con precio: " + Decimal.Parse(Preciotexto).ToString("0.00") + " S/?",
                    ShowCancelButton: true, owner: win) == false) return false;
            var plan = new PlanModel()
            {
                IdPlanes = planmodel.IdPlanes,
                descripcion = Descripcion,
                IdPlanDuracion = SelectedPlanDuracion.IdPlanDuracion,
                Precio = Decimal.Parse(Preciotexto),
                Servicios = Servicios.Where(x => x.IsSelected == true).AsList()
            };

            var result = await _helpexcep.ExcepHandler(() => _planservice.UpdatePlan(plan), win);

            _dialog.ShowDialog(result ? "Plan Modificado" : "Plan Rechazado");


            return result;
        }

        public void Limpiar() {
            SelectedPlanDuracion = null;
            Preciotexto = "";
            Preciotexto = "";
            Descripcion = null;
            Servicios.AsList().ForEach(x => x.IsSelected = false);
        }

        bool validacion() {
            if (!_helpers.IsNullOrWhiteSpaceAndDecimal(Preciotexto))
            {
                _dialog?.ShowDialog("Precio no tiene valor");
                return false;
            }

            if (SelectedPlanDuracion == null)
            {
                _dialog?.ShowDialog("Seleccione un Plan de Duracion");
                return false;
            }
            //reviso el conteo de todos aquellos que son true en IsSelected
            //si es cero es porque no han sido seleccionados
            if (Servicios.AsList().Count(x => x.IsSelected == true) == 0)
            {
                _dialog?.ShowDialog("Seleccione al menos un servicio");
                return false;
            }
            return true;
        }

        [RelayCommand]
        void SelectedAllServicios() {
            Servicios?.AsList().ForEach(x => x.IsSelected = true);
        }

        public void Dispose()
        {
          
            Servicios = null;
            Planduraciones = null;
            SelectedPlanDuracion = null;
        }
    }
}
