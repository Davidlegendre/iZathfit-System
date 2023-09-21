using iZathfit.Helpers;
using iZathfit.Views.Windows;
using Models;
using Services.Contratos;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace iZathfit.ViewModels.Pages.Negocio
{
    public partial class ContratosViewModel : ObservableObject
    {
        IContratosService? _service;
        localDialogService? _dialog;
        IExceptionHelperService? _helperexcep;

        public ContratosViewModel()
        {
            _service = App.GetService<IContratosService>();
            _dialog = App.GetService<localDialogService>();
            _helperexcep = App.GetService<IExceptionHelperService>();
        }

        [ObservableProperty]
        ObservableCollection<ContratoModel>? _contratos;

        public List<ContratoModel> _contratoslist = new List<ContratoModel>();

        [ObservableProperty]
        int _columns = 4;

        public async Task Getdata()
        {
            if (_service == null || _helperexcep == null) return;
            var result = await _helperexcep.ExcepHandler(() => _service.GetContratos(), App.GetService<MainWindow>());
            if (result != null)
            {
                _contratoslist = result;
            }
            else
                _contratoslist.Clear();
        }

        public void Ver(ContratoModel contrato)
        {
            _dialog?.ShowDialog(mensaje: "Persona: " + contrato.PersonaNombres + "\nIdentificacion: " + contrato.Identificacion
                + "\nPrecio del Plan: " + contrato.ValorOriginal.ToString("0.00") + " S/" + "\nDescuento de Promocion: " + contrato.Descuento + "%" +
                "\nTotal descontado: " + contrato.ValorTotal.ToString("0.00") + " S/" +
                "\nFecha de Inicio: " + contrato.FechaInicio.ToLongDateString()
                + "\nFecha de Fin: " + contrato.FechaFinal.ToLongDateString() + "\nValidez: " + (contrato.IsValid ? "Valido" : "No Valido")
                + (contrato.IsNotValid ? "\nRazon: " + contrato.DescripcionIsNotValid : ""),
                titulo: contrato.GetNombreContrato,
                owner: App.GetService<MainWindow>());
        }

        public async Task<bool> ValidarContrato(Guid idcontrato)
        {
            if (_service == null || _helperexcep == null) return false;
            if (_dialog?.ShowDialog("Desea Validar Otra Vez este contrato?", ShowCancelButton: true, owner: App.GetService<MainWindow>()) == false) return false;
            var result = await _helperexcep.ExcepHandler(() => _service.SetContratoNotValid(idcontrato, false, ""), App.GetService<MainWindow>());
            _dialog?.ShowDialog(result ? "Contrato Validado" : "Validacion rechazada", owner: App.GetService<MainWindow>());
            return result;
        }

        public async Task<bool> EliminarContrato(Guid idcontrato)
        {
            if (_service == null || _helperexcep == null) return false;
            if (_dialog?.ShowDialog("Desea Eliminar este contrato?", ShowCancelButton: true, owner: App.GetService<MainWindow>()) == false) return false;
            var result = await _helperexcep.ExcepHandler(() => _service.EliminarContrato(idcontrato), App.GetService<MainWindow>());
            _dialog?.ShowDialog(result ? "Eliminacion realizada" : "Eliminacion rechazada", owner: App.GetService<MainWindow>());
            return result;
        }

    }
}
