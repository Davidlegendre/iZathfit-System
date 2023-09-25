using Configuration.GlobalHelpers;
using iZathfit.ViewModels.Pages.Negocio;
using iZathfit.Views.Pages.Negocio.Ventanas.ViewModels;
using iZathfit.Views.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iZathfit.Views.Pages.Negocio
{
    /// <summary>
    /// Lógica de interacción para ServiciosPage.xaml
    /// </summary>
    public partial class ServiciosPage : UserControl, IDisposable
    {
        ServiciosPageVM? _vm;
        ServiciosViewModel? _svm;
        ObservableCollection<ServicioModel>? _copy;
        IGlobalHelpers? _helpers;
        public ServiciosPage()
        {
            InitializeComponent();
            _vm = this.DataContext as ServiciosPageVM;
            _svm= App.GetService<ServiciosViewModel>(); 
            _helpers = App.GetService<IGlobalHelpers>();
            this.Loaded += ServiciosPage_Loaded;
            this.SizeChanged += ServiciosPage_SizeChanged;
        }

        private void ServiciosPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_vm != null && _helpers != null)
            {
                _vm.Columns = _helpers.ColumnsFromWidthWindow(Convert.ToInt32(this.ActualWidth));
            }
        }

        private async void ServiciosPage_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 400);
            if (_vm != null)
               await _vm.CargarDatos();
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
                if (new Views.Pages.Negocio.Ventanas.ServiciosForm().ShowDialog() == true)
                {
                    _copy = null;
                    await _vm.CargarDatos();
                }
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (_svm != null && _vm != null)
            {
                var button = sender as Wpf.Ui.Controls.Button;
                var context = button?.DataContext as ServicioModel;
                if(context != null)
                {
                    if (await _svm.Eliminar(App.GetService<MainWindow>(), context))
                    {
                        _copy = null;
                        await _vm.CargarDatos();
                    }
                }
            }
        }

        private async void btnedit_Click(object sender, RoutedEventArgs e)
        {
            if (_svm != null && _vm != null)
            {
                var button = sender as Wpf.Ui.Controls.Button;
                var context = button?.DataContext as ServicioModel;
                if (context != null)
                {
                    if (new Negocio.Ventanas.ServiciosForm(context).ShowDialog() == true)
                    {
                        _copy = null;
                        await _vm.CargarDatos();
                    }
                }
            }
        }

        private void txtSearch_keydown(object sender, KeyEventArgs e)
        {
            if (_vm == null) return;
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    if (_copy == null) _copy = _vm.Servicios;

                    _vm.Servicios = new System.Collections.ObjectModel.ObservableCollection<ServicioModel>(
                        _copy.Where(x => x.NombreServicio.ToLower().Contains(txtSearch.Text.ToLower())));
                }
                else
                { 
                    if(_copy != null) _vm.Servicios = _copy;
                }
            }
        }

        public void Dispose()
        {
            _vm?.Dispose();
            _svm?.Dispose();
            _helpers = null;
            _copy = null;
        }
    }
}
