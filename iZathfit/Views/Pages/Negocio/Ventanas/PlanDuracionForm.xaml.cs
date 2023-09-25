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
    /// Lógica de interacción para PlanDuracionForm.xaml
    /// </summary>
    public partial class PlanDuracionForm : UiWindow, IDisposable
    {
        PlanDuracionModel? _model;
        PlanDuracionFormViewModel? _vm;
        localDialogService? _dialog;
        bool resultdialog = false;
        public PlanDuracionForm(PlanDuracionModel? model = null)
        {
            InitializeComponent();
            _model= model;
            _vm = App.GetService<PlanDuracionFormViewModel>();
            _dialog = App.GetService<localDialogService>();
            this.Loaded += PlanDuracionForm_Loaded;
            this.Closing += PlanDuracionForm_Closing;
        }

        private void PlanDuracionForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispose();
            DialogResult = resultdialog;
        }

        private void PlanDuracionForm_Loaded(object sender, RoutedEventArgs e)
        {
            tbtitle.Title = _model == null ? "Agregar" : "Modificar";
            if (_model != null)
            {
                txtdescripcion.Text = _model.descripcion;
                nbTiempoMeses.Value = _model.MesesTiempo;
                btnlimpiar.Visibility = Visibility.Collapsed;
                uniformgridpanel.Columns = 2;
                nbTiempoMeses.Focus();
            }
        }

        private async void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;

            if (_model != null)
            {
                _model.descripcion = txtdescripcion.Text;
                _model.MesesTiempo = Convert.ToInt32(nbTiempoMeses.Value);
            }

            var result = _model == null ? await _vm.Agregar(this, new PlanDuracionModel() {
                descripcion = txtdescripcion.Text,
                MesesTiempo = Convert.ToInt32(nbTiempoMeses.Value)
            }): await _vm.Modificar(this, _model);

            if (result)
            {
                resultdialog = true;
                Close();
            }
        }

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            resultdialog = false;
            Close();
        }

        void limpiar() {
            nbTiempoMeses.Clear();
            txtdescripcion.Clear();
            nbTiempoMeses.Focus();
        }

        bool validacion() {
            if (string.IsNullOrWhiteSpace(nbTiempoMeses.Text))
            {
                _dialog?.ShowDialog("Tiempo en meses no debe estar vacio");
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            _model = null;
            _vm?.Dispose();
            _dialog = null;
        }
    }
}
