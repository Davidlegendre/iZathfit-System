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
    /// Lógica de interacción para ServiciosForm.xaml
    /// </summary>
    public partial class ServiciosForm : UiWindow
    {
        localDialogService? _dialog;
        ServiciosViewModel? _viewmodel;
        ServicioModel? _model;
        bool resultdialog = false;
        public ServiciosForm(ServicioModel? model = null)
        {
            InitializeComponent();
            _dialog = App.GetService<localDialogService>();
            _viewmodel = App.GetService<ServiciosViewModel>();
            _model = model;
            this.Loaded += ServiciosForm_Loaded;
            this.Closing += ServiciosForm_Closing;
        }

        private void ServiciosForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = resultdialog;
        }

        private void ServiciosForm_Loaded(object sender, RoutedEventArgs e)
        {
            tbtitle.Title = _model == null ? "Agregar" : "Modificar";
            if (_model != null)
            {
                txtnombreservicio.Text = _model.NombreServicio;
                tphorarioinicio.TimeSelected = _model.HorarioInicio;
                tpHorarioFin.TimeSelected = _model.HorarioFin;
                tgbGrupal.IsChecked = _model.IsGrupal;
                txtnombreservicio.Focus();
                btnlimpiar.Visibility = Visibility.Collapsed;
                uniformgridpanel.Columns = 2;
            }
        }

        private async void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if (_viewmodel == null) return; 
            if (!validar()) return;
            if (_model != null)
            {
                _model.IsGrupal = tgbGrupal.IsChecked;
                _model.HorarioInicio = tphorarioinicio.TimeSelected;
                _model.HorarioFin = tpHorarioFin.TimeSelected;   
                _model.NombreServicio = txtnombreservicio.Text;
            }
            var result = _model == null ? await _viewmodel.Guardar(this, new ServicioModel()
            {
                NombreServicio = txtnombreservicio.Text,
                HorarioInicio = tphorarioinicio.TimeSelected,
                HorarioFin = tpHorarioFin.TimeSelected,
                IsGrupal = tgbGrupal.IsChecked
            }) : await _viewmodel.Modificar(this, _model);

            if (result)
            {
                resultdialog = true;
                Close();
            }
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            resultdialog= false;
            Close();
        }

        private void btnlimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        void limpiar() {
            txtnombreservicio.Clear();
            tphorarioinicio.TimeSelected = DateTime.Now;
            tpHorarioFin.TimeSelected = DateTime.Now;
            txtnombreservicio.Focus();
        }

        bool validar() {
            if (string.IsNullOrWhiteSpace(txtnombreservicio.Text))
            {
                _dialog?.ShowDialog("Nombre de Servicio esta vacio");
                return false;
            }

            if (tphorarioinicio.TimeSelected.TimeOfDay > tpHorarioFin.TimeSelected.TimeOfDay)
            {
                _dialog?.ShowDialog("El horario de inicio no puede ser mayor al horario de fin");
                return false;   
            }
            return true;
        }

        private void tgbGrupal_Checked(object sender, RoutedEventArgs e)
        {
            tgbGrupal.Content = "Grupal";
        }

        private void tgbGrupal_Unchecked(object sender, RoutedEventArgs e)
        {
            tgbGrupal.Content = "No es Grupal";
        }
    }
}
