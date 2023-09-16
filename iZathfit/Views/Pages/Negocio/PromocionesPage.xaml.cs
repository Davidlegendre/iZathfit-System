using iZathfit.ViewModels.Pages.Negocio;
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
    /// Lógica de interacción para PromocionesPage.xaml
    /// </summary>
    public partial class PromocionesPage : UserControl
    {
        PromocionViewModel? _vm;
        ObservableCollection<PromocionModelo>? _copy;
        public PromocionesPage()
        {
            InitializeComponent();
            _vm = this.DataContext as PromocionViewModel;
            this.Loaded += PromocionesPage_Loaded;
        }

        private async void PromocionesPage_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 400);
            if(_vm != null) { await _vm.GetData(); }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            if (new Negocio.Ventanas.PromocionForm().ShowDialog() == true)
            {
                _copy = null;
                await _vm.GetData();
            }
        }

        private void txtSearch_keydown(object sender, KeyEventArgs e)
        {
            if (_vm == null) return;
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    if (_copy == null) _copy = _vm.Promociones;

                    _vm.Promociones = new System.Collections.ObjectModel.ObservableCollection<PromocionModelo>(
                        _copy.Where(x => x.GetTitulo.ToLower().Contains(txtSearch.Text.ToLower())));
                }
                else
                {
                    if (_copy != null) _vm.Promociones = _copy;
                }
            }
        }

        private async void btnedit_Click(object sender, RoutedEventArgs e)
        {
            if(_vm== null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as PromocionModelo;
            if (context != null)
                if (new Ventanas.PromocionForm(context).ShowDialog() == true)
                {
                    _copy = null;
                    await _vm.GetData();
                }
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (_vm == null) return;
            var btn = (Wpf.Ui.Controls.Button)sender;
            var context = btn.DataContext as PromocionModelo;
            if (context != null)
                await _vm.Eliminar(context.IdPromocion);
        }
    }
}
