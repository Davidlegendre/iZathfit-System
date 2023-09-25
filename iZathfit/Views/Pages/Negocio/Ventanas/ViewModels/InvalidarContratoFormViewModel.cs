using iZathfit.Helpers;
using Models;
using Services.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Negocio.Ventanas.ViewModels
{
    public partial class InvalidarContratoFormViewModel : ObservableObject, IDisposable
    {
        IContratosService? _service;
        IExceptionHelperService? _helperexcep;
        localDialogService? _dialog;
        public InvalidarContratoFormViewModel()
        {
            _service = App.GetService<IContratosService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
            _dialog = App.GetService<localDialogService>();
        }

        [ObservableProperty]
        string? _textdescription = "";

        public async Task<bool> GuardarInvalidacion(UiWindow win, ContratoModel contrato) {
            if (_service == null || _helperexcep == null || _dialog == null) return false;
            if(!validar(win)) return false;
            var result = await _helperexcep.ExcepHandler(() => _service.SetContratoNotValid(contrato.IdContrato, true, Textdescription), win);
            _dialog.ShowDialog(result ? "Invalidacion guardada" : "Invalidacion Rechazada");
            return result;
        }

        bool validar(UiWindow win) {
            if (string.IsNullOrWhiteSpace(Textdescription))
            { 
                _dialog?.ShowDialog("Escriba una descripcion del porque esta invalidando este contrato", owner: win);
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            _service = null;
            _helperexcep =null;
            _dialog = null;
        }
    }
}
