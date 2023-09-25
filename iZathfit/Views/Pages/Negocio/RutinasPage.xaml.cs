using Configuration.GlobalHelpers;
using iZathfit.ViewModels.Pages.Negocio;
using iZathfit.Views.Pages.Negocio.Ventanas;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iZathfit.Views.Pages.Negocio
{
    /// <summary>
    /// Lógica de interacción para RutinasPage.xaml
    /// </summary>
    public partial class RutinasPage : UserControl, IDisposable
    {
        RutinaPageViewModel? _vm;
        IGlobalHelpers? _helpers;
        public RutinasPage()
        {
            InitializeComponent();
            _vm = DataContext as RutinaPageViewModel;
            _helpers = App.GetService<IGlobalHelpers>();
            this.Loaded += RutinasPage_Loaded;
            this.SizeChanged += RutinasPage_SizeChanged;
        }

        private void RutinasPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_vm != null && _helpers != null)
            {
                _vm.Columns = _helpers.ColumnsFromWidthWindow(Convert.ToInt32(this.ActualWidth));
            }
        }

        private void RutinasPage_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 300);
            if (_vm != null)
            {
                dtpicker.DateSelect = _vm.Fecha;
            }
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Wpf.Ui.Controls.Button;
            var context = button?.DataContext as RutinaModel;
            if (context != null && _vm != null)
            {
                await _vm.DeleteRutinaCommand.ExecuteAsync(context);
            }
        }

        private async void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Wpf.Ui.Controls.Button;
            var context = button?.DataContext as RutinaModel;
            if (context != null && _vm != null)
            {
                if (new Rutina(context).ShowDialog() == true)
                {
                    await _vm.RefreshRutinasCommand.ExecuteAsync(null);
                }
            }
        }

        private void DatePicker_DateSelectChanged(object sender, DateTime e)
        {
            if (_vm != null)
                _vm.Fecha = e;
        }

        public void Dispose()
        {
            _vm?.Dispose();
            _helpers = null;
        }
    }
}
