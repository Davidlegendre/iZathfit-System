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
    /// Lógica de interacción para PlanForm.xaml
    /// </summary>
    public partial class PlanForm : UiWindow
    {
        PlanModel? _model;
        bool resultdialog = false;
        PlanFormViewModel? _vm;
        localDialogService? _dialog;
        public PlanForm(PlanModel? model = null)
        {
            InitializeComponent();
            _model = model;
            _vm = this.DataContext as PlanFormViewModel;
            _dialog = App.GetService<localDialogService>();
            this.Loaded += PlanForm_Loaded;
            this.Closing += PlanForm_Closing;
        }

        private void PlanForm_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = resultdialog;
        }

        private async void PlanForm_Loaded(object sender, RoutedEventArgs e)
        {
            tbtitle.Title = _model == null ? "Agregar" : "Modificar";
         
            if (_vm != null)
            {
                if (!await _vm.Cargar(this, _model))
                {
                    resultdialog= false;
                    this.Close();
                }
            }
        }

        private async void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if(_vm != null && _dialog != null)
            {
                if (_dialog.ShowDialog("Desea " + (_model == null ? "Agregar" : "Modificar") +  " este Plan/Paquete?", 
                    ShowCancelButton: true, owner: this) == true)
                {
                    var result = _model == null ? await _vm.Guardar(this) : await _vm.Modificar(this, _model);
                    if (result)
                    {
                        resultdialog = true;
                        this.Close();
                    }
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
                _vm.Limpiar();
                
            }
        }

        
    }
}
