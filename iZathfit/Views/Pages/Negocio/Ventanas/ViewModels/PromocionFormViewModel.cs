using iZathfit.Helpers;
using Models;
using Services.Plan;
using Services.Promocion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Negocio.Ventanas.ViewModels
{
    public partial class PromocionFormViewModel : ObservableObject, IDisposable
    {
        IPlanService? _planservice;
        IExceptionHelperService? _helperexcep;
        IPromocionService? _servicio;
        localDialogService? _dialog;
        public PromocionFormViewModel()
        {
            _planservice = App.GetService<IPlanService>();
            _dialog = App.GetService<localDialogService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
            _servicio = App.GetService<IPromocionService>();
        }

        [ObservableProperty]
        ObservableCollection<PlanModel>? _planes;

        [ObservableProperty]
        PlanModel? _planselected = null;

        [ObservableProperty]
        string? _percentText = "";

        [ObservableProperty]
        bool? _activechecked = false;

        [ObservableProperty]
        string? _description = "";

        [ObservableProperty]
        DateTime? _selectedDate = null;

        public async Task<bool> GetData(UiWindow win, PromocionModelo? promo = null) { 
            if(_planservice == null || _dialog == null || _helperexcep == null) return false;
            var plns = await _helperexcep.ExcepHandler(() => _planservice.GetPlanesWithoutServices(), win);
            if (plns == null) {
                _dialog.ShowDialog("No podemos continuar ya que no hay planes, agregue unos primero", owner: win);
                return false;
            }

            Planes = new ObservableCollection<PlanModel>(plns);

            if(promo != null)
            {
                Planselected = Planes.First(x => x.IdPlanes == promo.IdPlan);
                PercentText = promo.DescuentoPercent.ToString();
                Activechecked = promo.DuracionTiempo.Date >= DateTime.Now.Date;
                //SelectedDate = promo.DuracionTiempo;
                Description = promo.descripcion;
            }

            return true;
        }

        public async Task<bool> Agregar(UiWindow win) {
            if (_servicio == null || _dialog == null || _helperexcep == null) return false;
            if (!validar()) return false;
            var promo = App.GetService<PromocionModelo>();
            promo.IdPlan = Planselected.IdPlanes;
            promo.DuracionTiempo = SelectedDate.Value;
            promo.DescuentoPercent = Convert.ToInt32(PercentText);
            promo.descripcion = Description;
            var result = await _helperexcep.ExcepHandler(() => _servicio.InsertPromocion(promo), win);
            _dialog.ShowDialog(result ? "Promocion Guardada" : "Promocion Rechazada");
            return result;
        }

        public async Task<bool> Modificar(UiWindow win, int idpromocion)
        {
            if (_servicio == null || _dialog == null || _helperexcep == null) return false;
            if (!validar()) return false;
            var promo = App.GetService<PromocionModelo>();
            promo.IdPromocion = idpromocion;
            promo.IdPlan = Planselected.IdPlanes;
            promo.DuracionTiempo = SelectedDate.Value;
            promo.DescuentoPercent = Convert.ToInt32(PercentText);
            promo.descripcion = Description;
            var result = await _helperexcep.ExcepHandler(() => _servicio.UpdatePromocion(promo), win);
            _dialog.ShowDialog(result ? "Promocion Modificada" : "Promocion Rechazada");
            return result;
        }

        bool validar()
        {
            if (Planselected == null) {
                _dialog?.ShowDialog("Seleccione un Plan");
                return false;
            }

            if (string.IsNullOrWhiteSpace(PercentText))
            {
                _dialog?.ShowDialog("Escriba algun descuento en porcentajes");
                return false;
            }

            if (SelectedDate == null)
            {
                _dialog?.ShowDialog("Seleccione alguna fecha para la duracion de la promo");
                return false;
            }

            if (SelectedDate.Value.Date < DateTime.Now.Date)
            {
                _dialog?.ShowDialog("La fecha tiene que ser mayor o igual a hoy, si la promo esta activada");
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            _planservice = null;
            _dialog = null;
            _helperexcep = null;
            _servicio = null;
            Planes = null;
            Planselected = null;
        }
    }
}
