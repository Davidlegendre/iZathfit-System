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
    public partial class ContratosForm : UiWindow
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
            DialogResult = resultdialog;
        }

        private async void ContratosForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            tbtitle.Title = "Agregar";
            if (await _vm.Cargardatos(this) == false) Close();
            searchidentificacion.txtbuscar.Focus();
            if (_vm.Personaslist != null)
            {
                searchidentificacion.SetCollectionToFind(_vm.Personaslist);
            }
        }

        private async void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            _vm.SelectedPersona = searchidentificacion.selectedPersona;
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
            searchidentificacion.Limpiar();
            _vm.SelectedPlan = null;
            _vm.SelectedPromo = null;
            _vm.Promocioneslist= null;
            _vm.EnableIfHavePromociones = false;
            _vm.TitlePromociones = "Promociones (No hay para el plan)";
            _vm.SelectedTipoPago = null;
            _vm.CodigoContrato = "";
            searchidentificacion.txtbuscar.Focus();
        }

        private void planSelect_Change(object sender, SelectionChangedEventArgs e)
        {
            if (_vm == null) return;
            _vm.Cargarpromociones();
            _vm.cargarDatosCalculados();
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
    }
}
