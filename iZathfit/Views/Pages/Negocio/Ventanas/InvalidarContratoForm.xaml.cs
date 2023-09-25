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
    /// Lógica de interacción para InvalidarContratoForm.xaml
    /// </summary>
    public partial class InvalidarContratoForm : UiWindow, IDisposable
    {
        ContratoModel _model;
        bool resultdialog= false;
        InvalidarContratoFormViewModel? _vm;
        public InvalidarContratoForm(ContratoModel model)
        {
            InitializeComponent();
            _model = model;
            _vm = DataContext as InvalidarContratoFormViewModel;
            this.Closing += InvalidarContratoForm_Closing;
        }

        private void InvalidarContratoForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispose();
            DialogResult = resultdialog;
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                if (await _vm.GuardarInvalidacion(this, _model) == true)
                {
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
            if (_vm != null)
            {
                _vm.Textdescription = "";
                txtdescripcion.Focus();
            }    
        }

        public void Dispose()
        {
            _vm?.Dispose();
        }
    }
}
