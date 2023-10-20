using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Services.Rutina;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iZathfit.ViewModels.Pages.Negocio
{
    public partial class RutinaPageViewModel : ObservableObject, IDisposable
    {
        public void Dispose()
        {
            Rutinaslist = null;
        }

        IRutinaService? _rutinaService;
        IExceptionHelperService? _helperexcep;
        localDialogService? _dialog;
        public RutinaPageViewModel()
        {
            _rutinaService = App.GetService<IRutinaService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
            _dialog = App.GetService<localDialogService>();
        }

        [ObservableProperty]
        ObservableCollection<RutinaModel>? _rutinaslist;

        [ObservableProperty]
        DateTime _fecha = DateTime.Now;

        [ObservableProperty]
        int? _columns = 4;

        [RelayCommand]
        async Task RefreshRutinas() {
            if (_rutinaService != null && _helperexcep != null)
            {
                var reuslt = await _helperexcep.ExcepHandler(() => _rutinaService.GetRutinasByFecha(Fecha), App.GetService<MainWindow>());
                Rutinaslist = reuslt != null? new ObservableCollection<RutinaModel>(reuslt) : null;
            }
        }

        [RelayCommand]
        async Task DeleteRutina(RutinaModel? rutina) {
            if (_rutinaService == null || _helperexcep == null || _dialog == null) return;
            if (_dialog.ShowDialog("Desea Eliminar esta Rutina?", ShowCancelButton: true, owner: App.GetService<MainWindow>()) == false) return; 
            if (rutina != null)
            {
                var resutl = await _helperexcep.ExcepHandler(() => _rutinaService.DeleteRutina(rutina.IdRutina), App.GetService<MainWindow>());
                _dialog.ShowDialog(resutl ? "Rutina Eliminada" : "Eliminacion rechazada", owner: App.GetService<MainWindow>());
                if (resutl)
                    await RefreshRutinasCommand.ExecuteAsync(null);
            }
        }
    }
}
