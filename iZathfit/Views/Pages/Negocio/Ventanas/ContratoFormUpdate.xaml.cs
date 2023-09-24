using iZathfit.Views.Pages.Negocio.Ventanas.ViewModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace iZathfit.Views.Pages.Negocio.Ventanas
{
    /// <summary>
    /// Lógica de interacción para ContratoFormUpdate.xaml
    /// </summary>
    public partial class ContratoFormUpdate : UiWindow
    {
        ContratoModel _model;
        ContratoFormViewModel? _vm;
        bool resultdialog = false;
        public ContratoFormUpdate(ContratoModel model)
        {
            InitializeComponent();
            _model = model;
            _vm = DataContext as ContratoFormViewModel;
            this.Loaded += ContratoFormUpdate_Loaded;
            this.Closing += ContratoFormUpdate_Closing;
        }

        private void ContratoFormUpdate_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = resultdialog;
        }

        private void ContratoFormUpdate_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                _vm.Dateselected = _model.FechaFinal;
                _vm.FechaFin = "Fecha Final Real: " + _model.FechaFinalReal.ToLongDateString();
                _vm.CodigoContrato = _model.NumeroContrato;
                datepicker.DateSelect = _vm.Dateselected;
                txtnumcontrato.Focus();
            }
        }

        private async void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            if (await _vm.UpdateContrato(this, _model.IdContrato) == true)
            {
                resultdialog = true;
                Close();
            }
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            resultdialog = false;
            Close();
        }

        private void datepicker_DateSelectChanged(object sender, DateTime e)
        {
            if (_vm == null) return;
            _vm.Dateselected = e.Date;
        }
    }
}
