using iZathfit.ViewModels.Pages.Negocio;
using iZathfit.Views.Pages.Negocio.Ventanas;
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
    /// Lógica de interacción para PlanesPage.xaml
    /// </summary>
    public partial class PlanesPage : UserControl
    {
        PlanViewModel? _vm;
        localDialogService? _dialog;
        ObservableCollection<PlanModel>? _copy;
        public PlanesPage()
        {
            InitializeComponent();
            _dialog = App.GetService<localDialogService>();
            _vm = this.DataContext as PlanViewModel;
            this.Loaded += PlanesPage_Loaded;
        }

        private async void PlanesPage_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 400);
            if(_vm != null)
            {
                if (!await _vm.CargarDatos())
                {
                    return; 
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
                    if (_copy == null) _copy = _vm.Planes;

                    _vm.Planes = new System.Collections.ObjectModel.ObservableCollection<PlanModel>(
                        _copy.Where(x => x.GetServicios.ToLower().Contains(txtSearch.Text.ToLower())
                        || x.GetPrecioString.ToLower().Contains(txtSearch.Text.ToLower())
                        || x.GetMesesTiempoString.ToLower().Contains(txtSearch.Text.ToLower())));
                }
                else
                {
                    if (_copy != null) _vm.Planes = _copy;
                }
            }
        }

        private async void btnedit_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            { 
                var btn = (Wpf.Ui.Controls.Button)sender;
                var context = btn.DataContext as PlanModel;
                if (context != null)
                    if (new PlanForm(context).ShowDialog() == true)
                    {
                        _copy = null;
                        await _vm.CargarDatos();
                    }
            }
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null && _dialog != null)
            {
                var btn = (Wpf.Ui.Controls.Button)sender;
                var context = btn.DataContext as PlanModel;
                if (context != null)
                    if (_dialog.ShowDialog("Desea Eliminar este Plan/Paquete?",
                    ShowCancelButton: true, owner: App.GetService<MainWindow>()) == true)
                    {
                        if (await _vm.Eliminar(App.GetService<MainWindow>(), context))
                        {
                            _copy = null;
                            await _vm.CargarDatos();
                        }
                    }
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                var btn = (Wpf.Ui.Controls.Button)sender;
                var context = btn.DataContext as PlanModel;
                if (context != null)
                    _vm.ViewData(context);
            }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(_vm != null)
            {
                if (new PlanForm().ShowDialog() == true)
                {
                    _copy = null;
                    await _vm.CargarDatos();
                }

            }
        }
    }
}
