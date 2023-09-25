using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels;
using Models;
using Models.DTOS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para MOcupacionesForm.xaml
    /// </summary>
    public partial class MPadecimientosEnfermedadesForm : UiWindow, IDisposable
    {
        localDialogService? _dialog;
        bool resultdialog = false;
        PadecimientosEnfermedadesDTO? _padecimiento;
        MPadecimientosEnfermedadesFormVM? _vm;
        public MPadecimientosEnfermedadesForm(
            PadecimientosEnfermedadesDTO? padecimiento = null)
        {
            InitializeComponent();
            _dialog = App.GetService<localDialogService>();
            
            _vm = this.DataContext as MPadecimientosEnfermedadesFormVM;
            _padecimiento = padecimiento;
            this.Loaded += MPadecimientosEnfermedadesForm_Loaded;
            this.Closing += MPadecimientosEnfermedadesForm_Closing;
        }

        private void MPadecimientosEnfermedadesForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispose();
            DialogResult = resultdialog;
        }

        private async void MPadecimientosEnfermedadesForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm != null) await _vm.CargarDatos(this, _padecimiento);
            TBTitulo.Title = _padecimiento == null ? "Agregar" : "Modifica";
            if (_padecimiento != null)
                Cbuscarpersona.PersonaSelected = _padecimiento.Persona;
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (_dialog.ShowDialog("Desea guardar esta ocupacion?", "Guardando", true) == true) {
                var result = _padecimiento == null? await _vm.Guardar(this) : await _vm.Editar(this);
                if (result)
                {
                    resultdialog = true;
                    this.Close();
                }
            }
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            _vm?.limpiar();
            txtpadecimiento.Focus();
            Cbuscarpersona.PersonaSelected = null;
        }

        private void btnaddpadecimiento_click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                _vm.agregarPadecimientoLocal();
            }
            txtpadecimiento.Focus();
        }

        private void btneliminar_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as Models.ModelsCommons.PadecimientoModelSencillo;
            if(context != null && _vm != null)
            {
                _vm.EliminarPadecimientoLocal(context);
            }
        }

        private void txtpadecimiento_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (_vm != null)
                {
                    _vm.Padecimientotxt = txtpadecimiento.Text;
                    _vm.agregarPadecimientoLocal();
                }
                txtpadecimiento.Focus();
            }
        }

        private void Cbuscarpersona_selectedChanged(object sender, PersonaModel e)
        {
            if (_vm != null)
            {
                _vm.Personamodel = e;
            }
        }

        public void Dispose()
        {
            _dialog = null;
            _padecimiento = null;
            _vm?.Dispose();
        }
    }
}
