using Configuration;
using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Models.ServiciodeModelos;
using Services.Promocion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iZathfit.ViewModels.Pages.Negocio
{
    public partial class PromocionViewModel : ObservableObject, IDisposable
    {
        IPromocionService? _servicio;
        IExceptionHelperService? _helperexec;
        localDialogService? _dialog;
        public PromocionViewModel()
        {
            _servicio = App.GetService<IPromocionService>();
            _helperexec = App.GetService<IExceptionHelperService>();
            _dialog = App.GetService<localDialogService>(); 
        }

        [ObservableProperty]
        ObservableCollection<PromocionModelo>? _promociones;

        [ObservableProperty]
        int _columns = 4;

        public async Task<bool> GetData() {
            if (_servicio == null || _helperexec == null) return false;
            var result = await _helperexec.ExcepHandler(() => _servicio.GetPromociones(), App.GetService<MainWindow>());
            Promociones = result == null ? null : new ObservableCollection<PromocionModelo>(result);

            if (Promociones != null)
                NotificadorServicesInModels.NotificarPromos(Promociones);
            return result != null;
        }


        public async Task Eliminar(int idPromo) {
            if (_servicio == null || _helperexec == null || _dialog == null) return;
            if (!_dialog.ShowDialog("Desea eliminar esta promocion?", ShowCancelButton: true, owner: App.GetService<MainWindow>())) return;
            //var confirm = _dialog.ShowConfirmPassword("Confirme la contraseña", _config.getuserSistema().contrasena, owner: App.GetService<MainWindow>());
            //if (!confirm) { _dialog.ShowDialog("Contraseña incorrecta"); return; }
            var result = await _helperexec.ExcepHandler(() => _servicio.DeletePromocion(idPromo), App.GetService<MainWindow>());
            if (result)
            {
                NotificadorServicesInModels.NotificarEliminaciondePromo(Promociones.First(x => x.IdPromocion == idPromo));
                Promociones.Remove(Promociones.First(x => x.IdPromocion == idPromo));
                
                
            }

        }

        public void Dispose()
        {
            Promociones = null;
        }
    }
}
