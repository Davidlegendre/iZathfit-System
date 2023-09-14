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
    /// Lógica de interacción para PlanDuracionPage.xaml
    /// </summary>
    public partial class PlanDuracionPage : UserControl
    {
        PlanDuracionPageViewModel? _vm;
        ObservableCollection<PlanDuracionModel>? _copy;
        PlanDuracionFormViewModel? _form;
        public PlanDuracionPage()
        {
            InitializeComponent();
            _vm = this.DataContext as PlanDuracionPageViewModel;
            _form = App.GetService<PlanDuracionFormViewModel>();
            this.Loaded += PlanDuracionPage_Loaded;
        }

        private async void PlanDuracionPage_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 400);
            if (_vm != null)
                await _vm.CargarDatos();
        }

        private void txtSearch_keydown(object sender, KeyEventArgs e)
        {
            if (_vm == null) return;
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    if (_copy == null) _copy = _vm.Planduracion;

                    _vm.Planduracion = new System.Collections.ObjectModel.ObservableCollection<PlanDuracionModel>(
                        _copy.Where(x => x.MesesTiempo.ToString().ToLower().Contains(txtSearch.Text.ToLower()) ||
                        x.GetMesesTiempoString.ToLower().Contains(txtSearch.Text.ToLower())));
                }
                else
                {
                    if (_copy != null) _vm.Planduracion = _copy;
                }
            }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            if (new Negocio.Ventanas.PlanDuracionForm().ShowDialog() == true)
            {
                _copy = null;
                await _vm.CargarDatos();
            }
        }

        private async void btnedit_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null || _form == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn?.DataContext as PlanDuracionModel;
            if(context == null) return; 
            if(new Negocio.Ventanas.PlanDuracionForm(context).ShowDialog() == true)
            {
                _copy = null;
                await _vm.CargarDatos();
            }
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (_form != null && _vm != null)
            {
                var button = sender as Wpf.Ui.Controls.Button;
                var context = button?.DataContext as PlanDuracionModel;
                if (context != null)
                {
                    if (await _form.Eliminar(App.GetService<MainWindow>(), context))
                    {
                        _copy = null;
                        await _vm.CargarDatos();
                    }
                }
            }
        }
    }
}
