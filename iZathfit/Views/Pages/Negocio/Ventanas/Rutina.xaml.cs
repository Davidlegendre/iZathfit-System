using iZathfit.Views.Pages.Negocio.Ventanas.ViewModels;
using iZathfit.Views.Windows;
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
    /// Lógica de interacción para Rutina.xaml
    /// </summary>
    public partial class Rutina : UiWindow, IDisposable
    {
        RutinasViewModel? _vm;
        bool resultdialog = false;
        RutinaModel? _model;
        public Rutina(RutinaModel? model = null)
        {
            InitializeComponent();
            _model = model;
            _vm = DataContext as RutinasViewModel;
            
            this.Loaded += Rutina_Loaded;
            this.Closing += Rutina_Closing;
        }

        private void Rutina_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispose();
            DialogResult = resultdialog;
        }

        private async void Rutina_Loaded(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            if (await _vm.GetData(this, _model) == false) Close();
            nbMonto.Focus();
        }

        private async void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if(_vm == null) return;
            var result = _model == null ? await _vm.InsertRutina(this) : await _vm.UpdateRutina(this, _model.IdRutina);
            if (result)
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

        public void Dispose()
        {
            _vm?.Dispose();
            _model = null;
            App.GetService<MainWindow>()?.Alzeimer();
        }
    }
}
