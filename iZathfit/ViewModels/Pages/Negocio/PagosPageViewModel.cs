using Configuration;
using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Services.SaldoXPersona;
using System.Collections.ObjectModel;

namespace iZathfit.ViewModels.Pages.Negocio
{
    public partial class PagosPageViewModel : ObservableObject
    {
        ISaldoXPersonaService? _service;
        localDialogService? _dialog;
        IExceptionHelperService? _helperexcep;
        IGeneralConfiguration? _config;
        public PagosPageViewModel()
        {
            _service = App.GetService<ISaldoXPersonaService>();
            _dialog = App.GetService<localDialogService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
            _config = App.GetService<IGeneralConfiguration>();
        }
        [ObservableProperty]
        ObservableCollection<SaldoXPersonaModel>? _saldoXPersonaslist;

        public List<SaldoXPersonaModel> _pagoslist = new List<SaldoXPersonaModel>();

        [ObservableProperty]
        int? _columns = 4;

        public async Task<bool> GetSaldosXPersona() {
            if (_service == null || _dialog == null || _helperexcep == null) return false;
            var result = await _helperexcep.ExcepHandler(() => _service.GetSaldoXPersonas(), App.GetService<MainWindow>());
            if (result != null)
                _pagoslist = result;
            else
                _pagoslist.Clear();
            return result != null;
        }

        public void View(SaldoXPersonaModel model)
        {
            _dialog?.ShowDialog(mensaje: model.GetNumeroContrato + "\nPersona: "+model.NombrePersona + "\n"
                + model.GetIdentificacion + "\n" + model.GetTotalContrato + "\n" + model.GetTotalFaltante + 
                "\n" + model.GetTotalPagado + "\n" + model.GetTipoPago + "\n" + model.GetFechaPago + "\n" + model.GetIsCompletePagado,
                titulo: "Pagos de: " + model.NombrePersona.Split(' ')[0], owner: App.GetService<MainWindow>());
        }

        public async Task<bool> Eliminar(SaldoXPersonaModel model) {
            if (_service == null || _dialog == null || _helperexcep == null || _config == null) return false;
            if(_dialog.ShowDialog("Desea eliminar este pago?", ShowCancelButton: true, owner: App.GetService<MainWindow>()) == false) return false;
            if (_dialog.ShowConfirmPassword("Confirme su contraseña", _config.getuserSistema().contrasena, owner: App.GetService<MainWindow>()) == false) return false;
            var result = await _helperexcep.ExcepHandler(() => _service.DeleteSaldoXPersona(model), App.GetService<MainWindow>());
            _dialog?.ShowDialog(result ? "Pago Eliminado" : "Pago Rechazado");
            return result;  
        }
    }
}
