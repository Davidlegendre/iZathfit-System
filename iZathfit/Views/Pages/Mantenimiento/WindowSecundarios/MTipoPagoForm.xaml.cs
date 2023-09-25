using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels;
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

namespace iZathfit.Views.Pages.Mantenimiento.WindowSecundarios
{
    /// <summary>
    /// Lógica de interacción para MTipoPagoForm.xaml
    /// </summary>
    public partial class MTipoPagoForm : UiWindow, IDisposable
    {
        MTipoPagoViewModel? _vm;
        TipoPagoModel? _tipopagomodel;
        bool resultdialog = false;
        public MTipoPagoForm(TipoPagoModel? TipoPago = null)
        {
            InitializeComponent();
            _vm = DataContext as MTipoPagoViewModel;
            _tipopagomodel = TipoPago;
            this.Loaded += MTipoPagoForm_Loaded;
            this.Closing += MTipoPagoForm_Closing;
        }

        private void MTipoPagoForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispose();
            DialogResult = resultdialog;
        }

        private void MTipoPagoForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
                _vm.Cargar(_tipopagomodel);
            txtTipoPago.Focus();
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                var result = _tipopagomodel != null ?
                    await _vm.Modificar(this, _tipopagomodel) :
                    await _vm.Guardar(this);
                if (result) {
                    resultdialog = true;
                    Close();
                }
            }
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            resultdialog = false;
            Close();
        }

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            if(_vm != null)
            {
                _vm.TipoPago = "";
                txtTipoPago.Focus();
            }
        }

        public void Dispose()
        {
            _vm?.Dispose();
            _tipopagomodel = null;
        }
    }
}
