using iZathfit.Views.Pages.Mantenimiento.WindowSecundarios.ViewModels;
using Models;
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
    public partial class MOcupacionesForm : UiWindow
    {
        localDialogService? _dialog;
        bool resultdialog = false;
        Ocupacion? _ocupacion;
        MOcupacionFormVM? _vm;
        List<Models.Ocupacion>? _ocupaciones;
        ObservableCollection<Models.Ocupacion>? _ocupacionescollection;
        public MOcupacionesForm(List<Models.Ocupacion>? ocupacions = null, ObservableCollection<Models.Ocupacion>? ocupacionescollection = null,
            Ocupacion? ocupacion = null)
        {
            InitializeComponent();
            _dialog = App.GetService<localDialogService>();
            _ocupacion = ocupacion;
            _vm = this.DataContext as MOcupacionFormVM;
            _ocupaciones = ocupacions;
            _ocupacionescollection = ocupacionescollection;
            this.Loaded += MOcupacionesForm_Loaded;
            this.Closing += MOcupacionesForm_Closing;
        }

        private void MOcupacionesForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = resultdialog;
        }

        private void MOcupacionesForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm != null) _vm.CargarDatos(_ocupacion);
            TBTitulo.Title = _ocupacion == null ? "Agregar" : "Modifica";
            txtocupacion.Focus();
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (_ocupaciones == null) return;
            if (_dialog.ShowDialog("Desea guardar esta ocupacion?", "Guardando", true) == true) {
                var result = _ocupacion == null ? await _vm.AgregarOcupacion(this, _ocupaciones, listacollection: _ocupacionescollection) :
                    await _vm.UpdateOcupacion(this, _ocupaciones, _ocupacion.IdOcupacion);
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
            _vm?.Limpiar();
            txtocupacion.Focus();
        }
    }
}
