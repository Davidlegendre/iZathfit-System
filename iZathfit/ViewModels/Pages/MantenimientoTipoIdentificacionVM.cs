using iZathfit.Helpers;
using Models;
using Services.TipoIdentificacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Pages
{
    public partial class MantenimientoTipoIdentificacionVM : ObservableObject
    {
        ITipoIdentificacionService? _servicio;
        localDialogService? _dialog;
        IExceptionHelperService? _helpexec;
        public MantenimientoTipoIdentificacionVM()
        {
            _servicio = App.GetService<ITipoIdentificacionService>();
            _dialog = App.GetService<localDialogService>();
            _helpexec= App.GetService<IExceptionHelperService>();
        }

        [ObservableProperty]
        ObservableCollection<Models.TipoIdentificacionModel>? _tipoIdentificacionlist;

        //mostrar, eliminar

        public async Task<bool> Cargardatos(UiWindow win) { 
            if(_servicio == null || _dialog == null || _helpexec == null) return false;
            var tipoiden = await _helpexec.ExcepHandler(() => _servicio.GetTipoIdentificaciones(), win);
            if (tipoiden == null) return false;
            TipoIdentificacionlist = new ObservableCollection<Models.TipoIdentificacionModel>(tipoiden);
            return true;
        }

        public async Task Eliminar(TipoIdentificacionModel tipo, UiWindow win) {
            if (_servicio == null || _dialog == null || _helpexec == null) return;
            var result = await _helpexec.ExcepHandler(() => _servicio.DeleteTipoIdentificacion(tipo.IdTipoIdentity), win);
            if (result == 0)
            {
                _dialog.ShowDialog("No se puede eliminar el tipo de indentificacion");
                
                return;
            }

            _dialog.ShowDialog("Tipo de Identificacion Eliminada Correctamente");
            TipoIdentificacionlist.Remove(TipoIdentificacionlist.First(x => x.IdTipoIdentity == tipo.IdTipoIdentity));
        }
    }
}
