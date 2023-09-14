using iZathfit.Helpers;
using Models;
using Services.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Negocio.Ventanas.ViewModels
{
    public class ServiciosViewModel
    {
        IServiciosService? _service;
        localDialogService? _dialog;
        IExceptionHelperService? _helperexcep;
        public ServiciosViewModel()
        {
            _service = App.GetService<IServiciosService>();
            _dialog = App.GetService<localDialogService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
        }

        public async Task<bool> Guardar(UiWindow ui, ServicioModel servicioModel) { 
            if(_service == null || _dialog == null || _helperexcep== null) return false;
            if(!_dialog.ShowDialog("Desea guardar el servicio: " + servicioModel.NombreServicio + "?", ShowCancelButton: true))
                return false;

            var result = await _helperexcep.ExcepHandler(() => _service.InsertServicios(servicioModel), ui);

            _dialog.ShowDialog(result ? "Servicio Añadido" : "Servicio no fue Añadido");
            return result;
        }

        public async Task<bool> Modificar(UiWindow ui, ServicioModel servicio) {
            if (_service == null || _dialog == null || _helperexcep == null) return false;
            if (!_dialog.ShowDialog("Desea modificar el servicio: " + servicio.NombreServicio + "?", ShowCancelButton: true))
                return false;

            var result = await _helperexcep.ExcepHandler(() => _service.UpdateServicio(servicio), ui);

            _dialog.ShowDialog(result ? "Servicio Modificado" : "Servicio no fue Modificado");
            return result;
        }

        public async Task<bool> Eliminar(UiWindow ui, ServicioModel servicio)
        {
            if (_service == null || _dialog == null || _helperexcep == null) return false;
            if (!_dialog.ShowDialog("Desea eliminar el servicio: " + servicio.NombreServicio + "?", ShowCancelButton: true))
                return false;

            var result = await _helperexcep.ExcepHandler(() => _service.DeleteServicio(servicio.IdServicio), ui);

            _dialog.ShowDialog(result ? "Servicio Eliminado" : "Servicio no fue Eliminado");
            return result;

        }
    }
}
