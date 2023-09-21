using Configuration.GlobalHelpers;
using Dapper;
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
    /// Lógica de interacción para PagosPage.xaml
    /// </summary>
    public partial class PagosPage : UserControl
    {
        IGlobalHelpers? _helpers;
        PagosPageViewModel? _vm;
        
        ObservableCollection<SaldoXPersonaModel> _copy = new ObservableCollection<SaldoXPersonaModel>();
        public PagosPage()
        {
            InitializeComponent();
            _vm = DataContext as PagosPageViewModel;
            _helpers = App.GetService<IGlobalHelpers>();
            this.SizeChanged += PagosPage_SizeChanged;
            this.Loaded += PagosPage_Loaded;
        }

        private async void PagosPage_Loaded(object sender, RoutedEventArgs e)
        {
            Wpf.Ui.Animations.Transitions.ApplyTransition(this, Wpf.Ui.Animations.TransitionType.FadeInWithSlide, 400);
            if (_vm != null)
            {
                await _vm.GetSaldosXPersona();
                _copy = new ObservableCollection<SaldoXPersonaModel>(_vm._pagoslist);
                _vm.SaldoXPersonaslist = paginator.GetPaginationCollection(_copy);
            }
        }

        private void PagosPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_vm != null && _helpers != null)
            {
                _vm.Columns = _helpers.ColumnsFromWidthWindow(Convert.ToInt32(this.ActualWidth));
            }
        }

        private void txtSearch_keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (_vm == null || _copy == null) return;
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                { _vm.SaldoXPersonaslist = paginator.GetPaginationCollection(_copy); }
                else
                {
                    _vm.SaldoXPersonaslist = paginator.BuscarWithPagination(_copy.Where(x =>
                        x.NombrePersona.ToLower().Contains(txtSearch.Text.ToLower()) 
                        || x.Identificacion.ToLower().Contains(txtSearch.Text.ToLower()) 
                        || x.NumeroContrato.ToLower().Contains(txtSearch.Text.ToLower())));
                }
            }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_vm != null)
            {
                if (new Negocio.Ventanas.SaldoXPersonaForm().ShowDialog() == true)
                {
                    await _vm.GetSaldosXPersona();
                    _copy = new ObservableCollection<SaldoXPersonaModel>(_vm._pagoslist);
                    _vm.SaldoXPersonaslist = paginator.GetPaginationCollection(_copy);
                }
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Wpf.Ui.Controls.Button;
            var context = btn?.DataContext as SaldoXPersonaModel;
            if(context != null && _vm != null)
            {
                _vm.View(context);
            }
        }

        private async void btndelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Wpf.Ui.Controls.Button;
            var context = btn?.DataContext as SaldoXPersonaModel;
            if (context != null && _vm != null)
            {
                if (await _vm.Eliminar(context) == true)
                {
                    await _vm.GetSaldosXPersona();
                    _copy = new ObservableCollection<SaldoXPersonaModel>(_vm._pagoslist);
                    _vm.SaldoXPersonaslist = paginator.GetPaginationCollection(_copy);
                }
            }
        }

        private void paginator_ChangePageEvent(object sender, EventArgs e)
        {
            if (_vm != null && _copy != null)
            {
                _vm.SaldoXPersonaslist = paginator.GetPaginationCollection(
                    new ObservableCollection<SaldoXPersonaModel>(_copy));
            }
        }
    }
}
