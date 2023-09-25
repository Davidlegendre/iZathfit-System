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
    /// Lógica de interacción para ContratosForm.xaml
    /// </summary>
    public partial class ContratosForm : UiWindow, IDisposable
    {
        ContratoFormViewModel? _vm;
        bool resultdialog = false;
        public ContratosForm()
        {
            InitializeComponent();
            _vm = DataContext as ContratoFormViewModel;
            this.Loaded += ContratosForm_Loaded;
            this.Closing += ContratosForm_Closing;
        }

        private void ContratosForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispose();
            DialogResult = resultdialog;
        }

        private async void ContratosForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            tbtitle.Title = "Agregar";
            if (await _vm.Cargardatos(this) == false) Close();
            datepicker.DateSelect = _vm.Dateselected;
        }

        private async void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            if (await _vm.Guardar(this) == true)
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

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            _vm.SelectedPlan = null;
            _vm.SelectedPromo = null;
            _vm.Promocioneslist= null;
            _vm.EnableIfHavePromociones = false;
            _vm.TitlePromociones = "Promociones (No hay para el plan)";
            _vm.SelectedTipoPago = null;
            _vm.CodigoContrato = "";
            _vm.Dateselected = DateTime.Now;
            Cbuscapersona.PersonaSelected = null;
        }

        private void planSelect_Change(object sender, SelectionChangedEventArgs e)
        {
            if (_vm == null) return;
            _vm.Cargarpromociones();
            _vm.cargarDatosCalculados();
            datepicker.DateSelect = _vm.Dateselected;
        }

        private void btnQuitarPromoSelected_click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            _vm.SelectedPromo = null;
        }

        private void promos_selectedchange(object sender, SelectionChangedEventArgs e)
        {
            if (_vm == null) return;
            _vm.cargarDatosCalculados();
        }

        private void datepicker_DateSelectChanged(object sender, DateTime e)
        {
            if(_vm == null) return;
            _vm.Dateselected = e.Date;
        }

        private void BuscarPersonaComponent_selectedChanged(object sender, PersonaModel e)
        {
            if (_vm != null)
            {                
                _vm.SelectedPersona = e;
            }
        }

        public void Dispose()
        {
            _vm?.Dispose();
        }
    }
}
